using LoanCar.Api.Models;
using LoanCar.Api.Services;
using LoanCar.Shared.Requests;
using LoanCar.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/*
 *  /-------\
 *  | R I P |
 *  |       |
 *  |       |
 *  |       |
 * -----------
 * 
 * Here lie the cool as shit compiler directives that where here. If you wish
 * to see them in their full glory please go to commit 06df92f
 */

namespace LoanCar.Api.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize]
    public class UserController(UserService userService) : ControllerBase
    {
        private readonly UserService userService = userService;

        [HttpGet]
        public IActionResult Get()
        {
            var users = userService.GetUsers().Select(u => new PublicUserDTO()
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IsAdmin = u.IsAdmin
            }).ToList();

            return Ok(users);
        }

        // TODO: Write a DTO for this
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var user = userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new PublicUserDTO()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsAdmin = user.IsAdmin
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewUserDTO dto)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                IsAdmin = dto.IsAdmin,
                Password = password
            };

            userService.AddUser(user);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UpdateUserDTO dto)
        {
            try
            {
                userService.UpdateUser(id, new User()
                {
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    IsAdmin = dto.IsAdmin
                });
            }
            catch
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                userService.DeleteUser(id);
            }
            catch
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
