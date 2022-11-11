using MusalaSoft.Transpotation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusalaSoft.Transpotation.BusinessServices.Contract
{
    public interface IMedicationDispatchService
    {
        int LoadMedications(Drone drone, IList<Medication> medications);

        IList<Medication> GetLoadedMedications(Drone drone);
    }
}
