using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class CarDBService : ICarDBService
    {
        private readonly WebAppDBContext _db;
        public CarDBService(WebAppDBContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _db.Cars.ToListAsync();
        }

        public async Task<Car> Get(int id)
        {
            return await _db.Cars.FirstOrDefaultAsync(x => x.Id == id);
            //return _db.Cars.FirstOrDefault(x => x.Id == id);
        }
        public async Task Update(Car car)
        {
            var tempCar = await Get(car.Id);
            if (tempCar != null)
            {
                tempCar.Brand = car.Brand;
                tempCar.Model = car.Model;
                tempCar.Year = car.Year;
                await _db.SaveChangesAsync();
            }
        }
        public async Task Add(Car car)
        {
            await _db.Cars.AddAsync(car);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var car = await Get(id);
            if (car != null)
            {
                _db.Cars.Remove(car);
                await _db.SaveChangesAsync();
            }
        }
    }
}
