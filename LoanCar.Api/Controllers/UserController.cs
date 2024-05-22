using LoanCar.Api.DTOs;
using LoanCar.Api.Models;
using LoanCar.Shared;
using Microsoft.AspNetCore.Mvc;

namespace LoanCar.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController(LoanCarContext db) : ControllerBase
    {
        private readonly LoanCarContext db = db;

        [HttpGet]
        public IActionResult Get()
        {
            var UserDTOs = db.Users.Select(u => new UserDTO()
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
                return Ok(db.Users.FirstOrDefault((u) => u.Id == id));
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

#if DEBUG
#warning Insecure endpoint!
#else
#error Insecure endpoint!
#endif
        [HttpPost]
        public IActionResult Post([FromBody] NewUserDTO userDTO)
        {
            User user = new()
            {
                Email = userDTO.Email,
                Name = userDTO.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password)
            };

            db.Users.Add(user);
            db.SaveChanges();

            return Created();
        }
    }
}

