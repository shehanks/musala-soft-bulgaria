using MusalaSoft.Transpotation.Domain.Models;

namespace MusalaSoft.Transpotation.Repository.Contract
{
    public interface IDroneTripDao : IRepository<DroneTripDataModel>
    {
        DroneTrip SaveDroneTrip(DroneTrip droneTrip);

        DroneTrip GetDroneTripById(int id);

        DroneTrip GetDroneTripByDroneId(int droneId);
    }
}
