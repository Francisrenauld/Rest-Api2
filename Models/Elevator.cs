namespace RocketElevators.Models
{
    public class Elevator
    {
        public long Id { get; set; }
        public string? Status { get; set; }
        public long column_id { get; set; }
        public Column? Columns { get; set; }
    }
}