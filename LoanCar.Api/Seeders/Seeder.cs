using LoanCar.Api.Models;

namespace LoanCar.Api.Seeders
{
    public static class Seeder
    {
        public static void Seed(this LoanCarContext db)
        {
            AdminSeeder.Seed(db);
        }
    }
}
