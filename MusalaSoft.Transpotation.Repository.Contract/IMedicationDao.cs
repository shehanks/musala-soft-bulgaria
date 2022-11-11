using MusalaSoft.Transpotation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusalaSoft.Transpotation.Repository.Contract
{
    public interface IMedicationDao : IRepository<MedicationDataModel>
    {
        bool SaveMedications(int droneTripId, IList<Medication> medications);

        IList<Medication> GetTripMedications(int? droneTripId);
    }
}
