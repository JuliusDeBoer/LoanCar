using LoanCar.Api.Models;
using LoanCar.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LoanCar.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private static readonly List<User> Users =
        [
            new User { Name = "John Doe", Email = "johndoe@example.com", Password = "Welkom01"}
        ];

        [HttpGet]
        public IActionResult Get()
        {
            var UserDTOs = Users.Select(u => new UserDTO()
            {
                Name = u.Name,
                Email = u.Email
            });
            return Ok(UserDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(Users[id - 1]);
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            Users.Add(user);
            return Created();
        }
    }
}

