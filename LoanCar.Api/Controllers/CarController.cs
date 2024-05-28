using LoanCar.Api.DTOs;
using LoanCar.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanCar.Api.Controllers
{
    [ApiController]
    [Route("cars")]
    public class CarController(LoanCarContext db) : ControllerBase
    {
        private readonly LoanCarContext db = db;

        [HttpGet]
        public IActionResult Get()
        {
            var cars = db.Cars.Select(c => new PublicCarDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Milage = c.Milage
            }).ToList();

            return Ok(cars);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var car = db.Cars.FirstOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(new PublicCarDTO()
            {
                Id = car.Id,
                Name = car.Name,
                Milage = car.Milage
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewCarDTO dto)
        {
            var car = new Car()
            {
                Name = dto.Name,
                Milage = dto.Milage
            };

            db.Cars.Add(car);

            db.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] NewCarDTO dto)
        {
            var car = db.Cars.FirstOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            car.Name = dto.Name;
            car.Milage = dto.Milage;

            db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var car = db.Cars.Where(c => c.Id == id);

            if (!car.Any())
            {
                return NotFound();
            }

            car.ExecuteDelete();

            db.SaveChanges();

            return Ok();
        }
    }
}
