using Microsoft.AspNetCore.Mvc;
using PensionHackathonBackend.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using PensionHackathonBackend.Contracts.UserContract;
using System.Linq;

namespace PensionHackathonBackend.Controllers
{
    [ApiController, Route("[controller]")]
    public class UserController(IUserService userService)
        : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetUsers()
        {
            var users = await _userService.GetAllUsers();

            var response = users.Select(user => new UserResponse(user.Id, user.Login, user.Password, user.Role));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateUser(
            [FromBody] UserRequest request)
        {
            var (user, error) = Core.Models.User.Create(
                Guid.NewGuid(),
                request.Login,
                request.Password,
                request.Role);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var userId = await _userService.CreateUser(user);

            return Ok(userId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateUser(Guid id,
            [FromBody] UserRequest request)
        {
            return Ok(await _userService.UpdateUser(id, request.Login, request.Password, request.Role));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteUser(Guid id)
        {
            return Ok(await _userService.DeleteUser(id));
        }
    }
}
