namespace RocketElevators.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string? Email_Of_The_Company_Contact { get; set; }
        
        public List<Building>? Buildings { get; set; }
    }
}