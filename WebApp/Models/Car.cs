namespace WebApp.Models
{
    public class Car
    {
        public static List<Car> list { get; set; } = new List<Car>();
        public int Id { get; set; }
        public string Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
    }
}
