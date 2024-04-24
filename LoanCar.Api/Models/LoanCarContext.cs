using Microsoft.EntityFrameworkCore;

namespace LoanCar.Api.Models
{
    public class LoanCarContext(DbContextOptions<LoanCarContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
