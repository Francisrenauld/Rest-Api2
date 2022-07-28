namespace RocketElevators.Models
{
    public class Building
    {
        public long Id { get; set; }
        public string? Full_Name_Of_The_Building_Administrator { get; set; }

        public string? Email_Of_The_Administrator_Of_The_Building { get; set; }

        public string? Phone_Number_Of_The_Building_Administrator { get; set; }

        public string? Full_Name_Of_The_Technical_Contact_For_The_Building { get; set; }

        public string? Technical_Contact_Email_For_The_Building { get; set; }

        public string? Technical_Contact_Phone_For_The_Building { get; set; }

        public long customer_id { get; set; }

        public long address_id { get; set; }

        public List<Battery> Batteries { get; set; }

    }
}