using Interfaces;
using Logic.Entities;

namespace Logic.ExtensionMethods
{
    public static class UserEM
    {
        public static UserDto toDto(this User user)
        {
            UserDto dto = new UserDto()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
            };
            return dto;
        }

        public static User toUser(this UserDto dto)
        {
            User user = new User(dto.UserId, dto.FirstName, dto.LastName, dto.Email);
            return user;
        }
    }
}
