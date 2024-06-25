using LoanCar.Api.Models;
using LoanCar.Shared.Requests;
using LoanCar.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Prevent this code from going into production with insecure endpoints.
// Remove if you dare!
#if DEBUG
#warning Very insecure endpoints
#else
#error Very insecure endpoints
#endif

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
            var users = db.Users.Select(u => new PublicUserDTO()
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
            var user = db.Users.FirstOrDefault(u => u.Id == id);

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

            db.Users.Add(user);

            db.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UpdateUserDTO dto)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.Email = dto.Email;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.IsAdmin = dto.IsAdmin;

            db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var user = db.Users.Where(u => u.Id == id);

            if (!user.Any())
            {
                return NotFound();
            }

            user.ExecuteDelete();

            db.SaveChanges();
            return Ok();
        }
    }
}
