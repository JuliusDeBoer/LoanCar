using LoanCar.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanCar.Api.Services
{
    public class UserService(LoanCarContext db)
    {
        private readonly LoanCarContext db = db;

        public IEnumerable<User> GetUsers()
        {
            return db.Users;
        }

        public User? GetUser(Guid id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUser(Guid id, User newUser)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id) ?? throw new Exception("Could not get user with ID");

            user.Email = newUser.Email;
            user.FirstName = newUser.FirstName;
            user.LastName = newUser.LastName;
            user.IsAdmin = newUser.IsAdmin;
            user.Password = newUser.Password;

            db.SaveChanges();
        }

        public void DeleteUser(Guid id)
        {
            var user = db.Users.Where(u => u.Id == id);

            if (!user.Any())
            {
                return;
            }

            user.ExecuteDelete();

            db.SaveChanges();
        }
    }
}
