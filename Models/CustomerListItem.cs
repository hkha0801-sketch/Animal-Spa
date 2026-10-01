namespace Animal_Spa.Models
{
    public class CustomerListItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int PetCount { get; set; }
    }
}
