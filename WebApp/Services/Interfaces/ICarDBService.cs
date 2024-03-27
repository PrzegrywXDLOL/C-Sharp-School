using WebApp.Models;

namespace WebApp.Services.Interfaces
{
    public interface ICarDBService
    {
        Task Add(Car car);
        Task Delete(int id);
        Task<Car> Get(int id);
        Task<IEnumerable<Car>> GetAll();
        Task Update(Car car);
    }
}