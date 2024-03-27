using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class CarService : ICarService
    {
        private readonly ICarDBService _db;

        public CarService(ICarDBService db)
        {
            _db = db;
        }

        private Car UpperLower(Car car)
        {
            car.Brand = car.Brand.ToUpper();
            car.Model = car.Model.ToLower();
            return car;
        }

        public async Task Add(Car car)
        {
            car = UpperLower(car);
            await _db.Add(car);
        }

        public async Task Delete(int id)
        {
            await _db.Delete(id);
        }

        public async Task<Car> Get(int id)
        {
            return await _db.Get(id);
        }
        public async Task Update(Car car)
        {
            car = UpperLower(car);
            await _db.Update(car);
        }
        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _db.GetAll();
        }
    }
}
