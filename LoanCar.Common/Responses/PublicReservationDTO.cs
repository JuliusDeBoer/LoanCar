namespace LoanCar.Shared.Responses
{
    public class PublicReservationDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public PublicUserDTO User { get; set; } = default!;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime StartingTime { get; set; } = default;
        public Guid CarId { get; set; }
        public PublicCarDTO Car { get; set; } = default!;
    }
}
