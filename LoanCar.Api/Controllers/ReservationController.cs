using LoanCar.Api.DTOs;
using LoanCar.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanCar.Api.Controllers
{
    [ApiController]
    [Route("reservations")]
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

        // FIXME: This exposes the password of the user. Make a DTO for this
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
                Car = reservation.Car,
                UserId = reservation.UserId,
                User = reservation.User,
                Origin = reservation.Origin,
                Destination = reservation.Destination,
                StartingTime = reservation.StartingTime,
            });
        }

        [HttpPost]
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
