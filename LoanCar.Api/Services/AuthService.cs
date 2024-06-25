using LoanCar.Api.Models;

namespace LoanCar.Api.Services
{
    public class AuthService(LoanCarContext db)
    {
        private readonly LoanCarContext db = db;

        public User Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email) ?? throw new Exception("Could not find user");

            if (!BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password))
            {
                throw new Exception("Invalid password");
            }

            return user;
        }
    }
}
