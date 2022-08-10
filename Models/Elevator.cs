namespace RocketElevators.Models
{
    public class Elevator
    {
        public long Id { get; set; }
        public string? Status { get; set; }
        public string? Model { get; set; }
        public string? Notes { get; set; }
        public long column_id { get; set; }
        public DateTime? Date_Of_Last_Inspection { get; set; }
        public Column? Columns { get; set; }
    }
}