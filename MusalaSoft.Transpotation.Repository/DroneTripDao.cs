using AutoMapper;
using MusalaSoft.Transpotation.DataAccess;
using MusalaSoft.Transpotation.Domain.Models;
using MusalaSoft.Transpotation.Repository.Contract;

namespace MusalaSoft.Transpotation.Repository
{
    public class DroneTripDao : Repository<DroneTripDataModel>, IDroneTripDao
    {
        private readonly IMapper _mapper;

        public DroneTripDao(ApplicationDBContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public DroneTrip GetDroneTripByDroneId(int droneId)
        {
            return _mapper.Map<DroneTrip>(Find(e => e.DroneId == droneId));
        }

        public DroneTrip GetDroneTripById(int id)
        {
            return _mapper.Map<DroneTrip>(Find(e => e.Id == id));
        }

        public DroneTrip SaveDroneTrip(DroneTrip droneTrip)
        {
            using (var context = new ApplicationDBContext())
            {
                var dataModel = _mapper.Map<DroneTripDataModel>(droneTrip);
                context.DroneTrip.Add(dataModel);
                context.SaveChanges();
                droneTrip.Id = dataModel.Id;
            }

            return droneTrip;
        }
    }
}
