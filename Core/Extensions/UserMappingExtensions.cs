using Core.Dtos;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    internal static class UserMappingExtensions
    {
        public static IEnumerable<UserDto> ToUserDtos(this IEnumerable<User> users)
        {
            return users.Select(e => e.ToUserDto());
        }

        public static UserDto ToUserDto(this User user)
        {
            return new()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }
    }
}
