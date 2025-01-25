using API.DTOs;
using API.Mappers;
using Core.Entities;
using Core.Interfaces;
using Core.Queries;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserRepository userRepository) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;

        [HttpGet("{id:guid}")]
        [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Get User by ID")]
        [EndpointDescription("Fetches the user details corresponding to the provided GUID.")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToDto());
        }

        [HttpGet]
        [ProducesResponseType<List<UserDto>>(StatusCodes.Status200OK)]
        [EndpointSummary("Get All Users")]
        [EndpointDescription("Fetches every user details")]

        public async Task<IActionResult> GetAll([FromQuery] UserQuery userQuery)
        {
            var (users, totalCount) = await _userRepository.GetAllAsync(userQuery);
            var usersDto = users.Select(u => u.ToDto()).ToList();
            var pagination = new
            {
                TotalCount = totalCount,
                PageNumber = userQuery.PageNumber,
                PageSize = userQuery.PageSize
            };

            Response.Headers["X-Pagination"] = JsonConvert.SerializeObject(pagination);

            return Ok(usersDto);
        }

        [HttpPost]
        [ProducesResponseType<UserDto>(StatusCodes.Status201Created)]
        [EndpointSummary("Create User")]
        [EndpointDescription("Creates a new user based on the provided user data.")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            var user = await _userRepository.CreateAsync(createUserDto.ToModelFromCreate());

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user.ToDto());
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Update User")]
        [EndpointDescription("Updates the user identified by the provided GUID and updated user data.")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserDto updateUserDto)
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

            var updatedUser = await _userRepository.UpdateAsync(id, user);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser.ToDto());
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Delete User")]
        [EndpointDescription("Deletes the user identified by the provided GUID.")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _userRepository.DeleteAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}