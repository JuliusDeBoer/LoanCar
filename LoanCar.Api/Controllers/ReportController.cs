using LoanCar.Api.Models;
using LoanCar.Shared.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoanCar.Api.Controllers
{
    [ApiController]
    [Route("reports")]
    public class ReportController(LoanCarContext db) : ControllerBase
    {
        private readonly LoanCarContext db = db;

        [HttpGet]
        public IActionResult Get()
        {
            var reports = db.Reports.ToList();

            return Ok(reports);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var report = db.Reports
                .Include(r => r.Reservation)
                .FirstOrDefault(r => r.Id == id);

            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewReportDTO dto)
        {
            var report = new Report()
            {
                Description = dto.Description,
                ReservationId = dto.ReservationId,
                Image = dto.Image
            };

            db.Reports.Add(report);

            db.SaveChanges();

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] NewReportDTO dto)
        {
            var report = db.Reports.FirstOrDefault(r => r.Id == id);

            if (report == null)
            {
                return NotFound();
            }

            report.Description = dto.Description;
            report.ReservationId = dto.ReservationId;
            report.Image = dto.Image;

            db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var report = db.Reports.Where(r => r.Id == id);

            if (!report.Any())
            {
                return NotFound();
            }

            report.ExecuteDelete();

            db.SaveChanges();

            return Ok();
        }
    }
}
