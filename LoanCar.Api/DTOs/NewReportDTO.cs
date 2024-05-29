namespace LoanCar.Api.DTOs
{
    public class NewReportDTO
    {
        public Guid ReservationId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty; // Base64 encoded
    }
}
