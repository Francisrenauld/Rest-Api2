namespace RocketElevators.Models
{
    public class Battery
    {
        public long Id { get; set; }
        public string? Status { get; set; }
        public string? Certificate_Of_Operations { get; set; }
        public string? Notes { get; set; }
        public string? Type { get; set; }
        public DateTime? Date_Of_Commissioning { get; set; }
        public long building_id { get; set; }
        public Building? Building { get; set; }
        public List<Column>? Columns { get; set; }

        
    }
}