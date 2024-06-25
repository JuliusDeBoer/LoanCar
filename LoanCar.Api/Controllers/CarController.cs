using LoanCar.Api.Models;
using LoanCar.Api.Services;
using LoanCar.Shared.Requests;
using LoanCar.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanCar.Api.Controllers
{
    [ApiController]
    [Route("cars")]
    [Authorize]
    public class CarController(CarService carService) : ControllerBase
    {
        private readonly CarService carService = carService;

        [HttpGet]
        public IActionResult Get()
        {
            var cars = carService.GetCars().Select(c => new PublicCarDTO()
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
            var car = carService.GetCar(id);

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
        [Authorize(Policy = "admin")]
        public IActionResult Post([FromBody] NewCarDTO dto)
        {
            var car = new Car()
            {
                Name = dto.Name,
                Milage = dto.Milage
            };

            carService.AddCar(car);

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "admin")]
        public IActionResult Put(Guid id, [FromBody] NewCarDTO dto)
        {
            try
            {
                carService.UpdateCar(id, new Car()
                {
                    Id = id,
                    Milage = dto.Milage,
                    Name = dto.Name
                });
            }
            catch
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                carService.DeleteCar(id);
            }
            catch
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
