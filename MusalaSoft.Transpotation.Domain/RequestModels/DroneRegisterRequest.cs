namespace MusalaSoft.Transpotation.Domain.RequestModels
{
    public class DroneRegisterRequest
    {
        public string SerialNumber { get; set; } = string.Empty;

        public int ModelId { get; set; }

        public decimal BatteryPercentage { get; set; }
    }
}
