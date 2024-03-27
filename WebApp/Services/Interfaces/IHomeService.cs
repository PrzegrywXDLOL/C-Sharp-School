using WebApp.Models;

namespace WebApp.Services.Interfaces
{
    public interface IHomeService
    {
        void AddToList(Car car);
        void RemoveToList(int id);
    }
}