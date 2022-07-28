namespace RocketElevators.Models
{
    public class Column
    {
        public long Id { get; set; }
        public string? Status { get; set; }
        public long battery_id { get; set; }
        public Battery? Batteries { get; set; }
        public List<Elevator>? Elevators { get; set; }
    }
}