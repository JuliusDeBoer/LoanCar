namespace LoanCar.Shared.Responses
{
    public class PublicCarDTO
    {
        public Guid Id { get; set; }
        public string Name { set; get; } = string.Empty;
        public float Milage { get; set; }
    }
}
