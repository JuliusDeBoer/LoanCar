using LoanCar.Api.Models;
using LoanCar.Shared.Requests;
using LoanCar.Shared.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanCar.Api.Controllers
{
    [ApiController]
    [Route("reservations")]
    [Authorize]
    public class ReservationController(LoanCarContext db) : ControllerBase
    {
        private readonly LoanCarContext db = db;

        [HttpGet]
        public IActionResult Get()
        {
            var reservations = db.Reservations.Select(r => new PublicReservationDTO()
            {
                Id = r.Id,
                CarId = r.CarId,
                UserId = r.UserId,
                Origin = r.Origin,
                Destination = r.Destination,
                StartingTime = r.StartingTime,
            }).ToList();

            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var reservation = db.Reservations
                .Include(r => r.User)
                .Include(r => r.Car)
                .FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(new PublicReservationDTO()
            {
                Id = reservation.Id,
                CarId = reservation.CarId,
                Car = new PublicCarDTO()
                {
                    Id = reservation.Car.Id,
                    Milage = reservation.Car.Milage,
                    Name = reservation.Car.Name
                },
                UserId = reservation.UserId,
                User = new PublicUserDTO()
                {
                    Id = reservation.User.Id,
                    Email = reservation.User.Email,
                    FirstName = reservation.User.FirstName,
                    LastName = reservation.User.LastName,
                    IsAdmin = reservation.User.IsAdmin
                },
                Origin = reservation.Origin,
                Destination = reservation.Destination,
                StartingTime = reservation.StartingTime,
            });
        }

        [HttpPost]
        [Authorize(Policy = "admin")]
        public IActionResult Post([FromBody] NewReservationDTO dto)
        {
            var reservation = new Reservation()
            {
                CarId = dto.CarId,
                UserId = dto.UserId,
                Origin = dto.Origin,
                Destination = dto.Destination,
                StartingTime = dto.StartingTime,
            };

            db.Reservations.Add(reservation);

            db.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "admin")]
        public IActionResult Put(Guid id, [FromBody] NewReservationDTO dto)
        {
            var reservation = db.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            reservation.CarId = dto.CarId;
            reservation.UserId = dto.UserId;
            reservation.Origin = dto.Origin;
            reservation.Destination = dto.Destination;
            reservation.StartingTime = dto.StartingTime;

            db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "admin")]
        public IActionResult Delete(Guid id)
        {
            var reservation = db.Reservations.Where(r => r.Id == id);

            if (!reservation.Any())
            {
                return NotFound();
            }

            reservation.ExecuteDelete();

            db.SaveChanges();

            return Ok();
        }
    }
}
