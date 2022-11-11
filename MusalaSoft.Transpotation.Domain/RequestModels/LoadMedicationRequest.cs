namespace MusalaSoft.Transpotation.Domain.RequestModels
{
    public class LoadMedicationRequest
    {
        public int droneId { get; set; }

        public IList<MedicationRequest> medicationItems { get; set; } = new List<MedicationRequest>();
    }
}
