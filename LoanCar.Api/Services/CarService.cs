using LoanCar.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanCar.Api.Services
{
    public class CarService(LoanCarContext db)
    {
        private readonly LoanCarContext db = db;

        public IEnumerable<Car> GetCars()
        {
            return db.Cars;
        }

        public Car? GetCar(Guid id)
        {
            return db.Cars.FirstOrDefault(c => c.Id == id);
        }

        public void AddCar(Car car)
        {
            db.Cars.Add(car);
            db.SaveChanges();
        }

        public void UpdateCar(Guid id, Car newCar)
        {
            var car = db.Cars.FirstOrDefault(c => c.Id == id) ?? throw new Exception("Could not get car with ID");

            car.Name = newCar.Name;
            car.Milage = newCar.Milage;

            db.SaveChanges();
        }

        public void DeleteCar(Guid id)
        {
            var car = db.Cars.Where(c => c.Id == id);

            if (!car.Any())
            {
                return;
            }

            car.ExecuteDelete();

            db.SaveChanges();
        }
    }
}
