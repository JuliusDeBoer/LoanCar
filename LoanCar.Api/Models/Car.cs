using System.ComponentModel.DataAnnotations;

namespace LoanCar.Api.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { set; get; } = string.Empty;
        public float Milage { get; set; }
    }
}
