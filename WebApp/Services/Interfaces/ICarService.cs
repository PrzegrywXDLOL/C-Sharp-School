using WebApp.Models;

namespace WebApp.Services.Interfaces
{
    public interface ICarService
    {
        Task Add(Car car);
        Task Delete(int id);
        Task<Car> Get(int id);
        Task Update(Car car);
        Task<IEnumerable<Car>> GetAll();
    }
}