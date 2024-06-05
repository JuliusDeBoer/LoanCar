using Microsoft.EntityFrameworkCore;

namespace LoanCar.Api.Models
{
    public class LoanCarContext(DbContextOptions<LoanCarContext> options) : DbContext(options)
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
