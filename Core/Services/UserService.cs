using Core.Dtos;
using DataAccess.Entities;
using DataLayer;

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
            var results = (await _unitOfWork.Users.GetAll()).Select(e => new UserDto()
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email
            }).ToList();

            return results;
        }

        public async Task Register(RegisterDto registerData)
        {
            var hashedPassword = _authService.HashPassword(registerData.Password);

            var user = new User
            {
                FirstName = registerData.FirstName,
                LastName = registerData.LastName,
                Email = registerData.Email,
                PasswordHash = hashedPassword,
                Role = DataAccess.Enums.UserRole.User
            };

            _unitOfWork.Users.Add(user);
            _unitOfWork.SaveChanges();
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
