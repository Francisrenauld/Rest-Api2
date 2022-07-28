namespace RocketElevators.Models
{
    public class Battery
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public long building_id { get; set; }
        public Building Building { get; set; }
        public List<Column>? Columns { get; set; }

        
    }
}