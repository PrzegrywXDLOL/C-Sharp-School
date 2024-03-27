using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class HomeService : IHomeService
    {
        public void AddToList(Car car)
        {
            car = UpperLower(car);
            Car.list.Add(car);
        }
        public void RemoveToList(int id)
        {
            if (Car.list[id] != null)
                Car.list.RemoveAt(id);
        }
        private Car UpperLower(Car car)
        {
            car.Brand = car.Brand.ToUpper();
            car.Model = car.Model?.ToLower();
            return car;
        }
    }
}
