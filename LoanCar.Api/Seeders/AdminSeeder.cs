using LoanCar.Api.Models;
using System.Text;

namespace LoanCar.Api.Seeders
{
    public static class AdminSeeder
    {
        public static void Seed(LoanCarContext db)
        {
            var hasAdmin = db.Users.Any(u => u.IsAdmin);

            if (hasAdmin)
            {
                return;
            }

            var password = GeneratePassword(5);

            // This should be done with an ILogger. Too bad!
            Console.Write("\n\n=== ADMIN CREDENTIALS ===\nUsername: admin@example.com\nPassword: " + password + "\n\n");

            db.Users.Add(new User()
            {
                FirstName = "Admin",
                LastName = "",
                Email = "admin@example.com",
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword(password),
                IsAdmin = true
            });

            db.SaveChanges();
        }

        private static string GeneratePassword(int length)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder res = new();
            Random rnd = new();

            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
