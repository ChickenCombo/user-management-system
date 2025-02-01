using API.DTOs.User;
using API.Helper;
using API.Mappers;
using Core.Entities;
using Core.Queries;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Handlers
{
    public static class UserHandler
    {
        public static async Task<Results<Ok<UserDto>, NotFound>> GetById(Guid id, IUnitOfWork uow)
        {
            var user = await uow.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(user.ToDto());
        }

        public static async Task<Ok<List<UserDto>>> GetAll([AsParameters] UserQuery userQuery, HttpResponse response, IUnitOfWork uow)
        {
            var (users, totalCount) = await uow.UserRepository.GetAllAsync(userQuery);
            var usersDto = users.Select(u => u.ToDto()).ToList();

            PaginationHelper.SetPaginationHeader(response, totalCount, userQuery.PageNumber, userQuery.PageSize);

            return TypedResults.Ok(usersDto);
        }

        public static async Task<Created<UserDto>> Create([FromBody] CreateUserDto createUserDto, HttpContext httpContext, IUnitOfWork uow)
        {
            var user = await uow.UserRepository.CreateAsync(createUserDto.ToModelFromCreate());

            await uow.SaveChangesAsync();

            string uri = $"{httpContext.Request.Path}/{user.Id}";

            return TypedResults.Created(uri, user.ToDto());
        }

        public static async Task<Results<Ok<UserDto>, NotFound>> Update([FromRoute] Guid id, [FromBody] UpdateUserDto updateUserDto, IUnitOfWork uow)
        {
            var user = new User
            {
                Id = id,
                FirstName = updateUserDto.FirstName,
                MiddleName = updateUserDto.MiddleName,
                LastName = updateUserDto.LastName,
                Email = updateUserDto.Email,
                Role = updateUserDto.Role,
            };

            var updatedUser = await uow.UserRepository.UpdateAsync(id, user);

            if (updatedUser == null)
            {
                return TypedResults.NotFound();
            }

            await uow.SaveChangesAsync();

            return TypedResults.Ok(updatedUser.ToDto());
        }

        public static async Task<Results<NotFound, NoContent>> Delete([FromRoute] Guid id, IUnitOfWork uow)
        {
            var user = await uow.UserRepository.DeleteAsync(id);

            if (user == null)
            {
                return TypedResults.NotFound();
            }

            await uow.SaveChangesAsync();

            return TypedResults.NoContent();
        }
    }
}