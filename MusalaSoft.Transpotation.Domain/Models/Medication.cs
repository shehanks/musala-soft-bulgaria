namespace MusalaSoft.Transpotation.Domain.Models
{
    public class Medication
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Weight { get; set; }

        public string Code { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public int DroneTripId { get; set; }
    }
}

