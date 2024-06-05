using LoanCar.Api.Models;

namespace LoanCar.Api.DTOs
{
    public class PublicReservationDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime StartingTime { get; set; } = default;
        public Guid CarId { get; set; }
        public Car Car { get; set; } = default!;
    }
}
