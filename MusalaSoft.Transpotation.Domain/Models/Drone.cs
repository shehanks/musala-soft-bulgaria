namespace MusalaSoft.Transpotation.Domain.Models
{
    public class Drone
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; } = string.Empty;

        public int ModelId { get; set; }

        public decimal BatteryPercentage { get; set; }

        public int StateId { get; set; }

        public int? CurrentTripId { get; set; }
    }
}
