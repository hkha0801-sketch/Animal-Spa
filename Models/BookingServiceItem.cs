namespace Animal_Spa.Models
{
    public class BookingServiceItem
    {
        public string ServiceName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string AnimalType { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string PriceText { get; set; } = string.Empty;
        public decimal PriceValue { get; set; }
        public string ServiceIcon { get; set; } = "/Animal-Spa;component/Asset/ICON-BATH.jpg";

        public string AnimalSummary => $"{AnimalType} - {Weight}";
    }
}
