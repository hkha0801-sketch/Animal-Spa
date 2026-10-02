using System.Collections.Generic;

namespace Animal_Spa.Models
{
    public class CustomerDetailItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public List<PetDetailItem> Pets { get; set; } = new();
    }

    public class PetDetailItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string AnimalIcon { get; set; } = string.Empty;
    }
}
