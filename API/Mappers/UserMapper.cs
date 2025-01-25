using API.DTOs.User;
using Core.Entities;

namespace API.Mappers
{
    public static class UserMapper
    {
        public static User ToModelFromCreate(this CreateUserDto createUserDto)
        {
            return new User
            {
                FirstName = createUserDto.FirstName,
                MiddleName = createUserDto.MiddleName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                Role = createUserDto.Role
            };
        }

        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };
        }

    }
}