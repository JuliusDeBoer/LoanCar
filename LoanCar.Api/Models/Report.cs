using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanCar.Api.Models
{
    public class Report
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ReservationId { get; set; }
        [ForeignKey(nameof(ReservationId))]
        public Reservation Reservation { get; set; } = default!;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty; // Base64 encoded
    }
}
