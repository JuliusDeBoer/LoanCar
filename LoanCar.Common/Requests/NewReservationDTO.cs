namespace LoanCar.Shared.Requests
{
    public class NewReservationDTO
    {
        public Guid UserId { get; set; }
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime StartingTime { get; set; } = default;
        public Guid CarId { get; set; }
    }
}
