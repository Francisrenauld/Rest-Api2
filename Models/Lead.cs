namespace RocketElevators.Models
{
    public class Lead
    {
        public long Id { get; set; }
        public string? Full_Name_Of_The_Contact { get; set; }

        public string? Company_Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Project_Name { get; set; }

        public string? Project_Description { get; set; }

        public string? Departement_In_Charge_Of_The_Elevators { get; set; }

        public string? Message { get; set; }

        public DateTime? created_at { get; set; }
    }
}