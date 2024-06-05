
using System.ComponentModel.DataAnnotations;

namespace LoanCar.Api.Models
{
    public class Notification
    {
        [Key]
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
