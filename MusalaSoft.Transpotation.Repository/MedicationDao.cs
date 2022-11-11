using AutoMapper;
using MusalaSoft.Transpotation.DataAccess;
using MusalaSoft.Transpotation.Domain.Models;
using MusalaSoft.Transpotation.Repository.Contract;

namespace MusalaSoft.Transpotation.Repository
{
    public class MedicationDao : Repository<MedicationDataModel>, IMedicationDao
    {
        private readonly IMapper _mapper;

        public MedicationDao(ApplicationDBContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public IList<Medication> GetTripMedications(int? droneTripId)
        {
            return _mapper.Map<List<Medication>>(FindList(x => x.DroneTripId == droneTripId));
        }

        public bool SaveMedications(int droneTripId, IList<Medication> medications)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    var dataModelList = _mapper.Map<List<MedicationDataModel>>(medications);

                    foreach (var model in dataModelList)
                    {
                        model.DroneTripId = droneTripId;
                        context.Medication.Add(model);
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
