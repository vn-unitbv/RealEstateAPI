using Core.Dtos;
using DataAccess.Entities;
using DataAccess;
using Infrastructure.Exceptions;
using Core.Extensions;
using DataAccess.Enums;
using System.Text.RegularExpressions;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly AuthorizationService _authService;

        public UserService(UnitOfWork unitOfWork, AuthorizationService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }
        public async Task<List<UserDto>> GetAll()
        {
            var results = (await _unitOfWork.Users.GetAll())
                .ToUserDtos()
                .ToList();

            return results;
        }

        public async Task<List<FeedAnnouncementDto>> GetAnnouncements(Guid userId)
        {
			var user = await _unitOfWork.Users.Get(userId);
			if (user == null)
				throw new ResourceMissingException($"User with id {userId} doesn't exist");


            //TODO: Write the filtering logic in the repository to improve efficiency (less data over network)
			var results = (await _unitOfWork.Announcements.GetAll())
                .Where(a => a.PosterId == userId)
                .ToFeedAnnouncementDtos()
                .ToList();

            return results;
        }

        public async Task<List<CommentDto>> GetComments(Guid userId)
        {
	        var user = await _unitOfWork.Users.Get(userId);
	        if (user == null)
		        throw new ResourceMissingException($"User with id {userId} doesn't exist");

			var results = (await _unitOfWork.Comments.GetAll())
		        .Where(c => c.Poster.Id == userId)
		        .ToCommentDtos()
		        .ToList();

            return results;
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var user = await _unitOfWork.Users.Get(id);
            if (user == null)
                throw new ResourceMissingException($"User with id {id} doesn't exist");

            return user.ToUserDto();
        }
        private async Task<bool> IsEmailTaken(string email)
        {
            List<UserDto> users = await GetAll();
            HashSet<string> emails = new HashSet<string>();
            foreach (var user in users)
            {
                emails.Add(user.Email);
            }
            return emails.Contains(email);
        }
        private bool IsEmailValid(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, emailPattern);
        }
        public async Task Register(RegisterDto registerData)
        {
            if (await IsEmailTaken(registerData.Email))
            {
                throw new EmailAlreadyRegisteredException(registerData.Email);
            }
            if (!IsEmailValid(registerData.Email))
            {
                throw new InvalidEmailFormatException(registerData.Email);
            }
            var hashedPassword = _authService.HashPassword(registerData.Password);
            var user = new User
            {
                FirstName = registerData.FirstName,
                LastName = registerData.LastName,
                Email = registerData.Email,
                PasswordHash = hashedPassword,
                Role = UserRole.User
            };

            _unitOfWork.Users.Add(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<string?> Validate(LoginDto payload)
        {
            var user = await _unitOfWork.Users.GetByEmail(payload.Email);

            var passwordFine = _authService.VerifyHashedPassword(user.PasswordHash, payload.Password);

            if (!passwordFine)
                return null;

            return _authService.GetToken(user);
        }
    }
}
