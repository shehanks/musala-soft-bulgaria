using MusalaSoft.Transpotation.Domain.Models;

namespace MusalaSoft.Transpotation.BusinessServices.Contract
{
    public interface IDroneService
    {
        IList<Drone> GetAllDrones();

        IList<Drone> GetAvailableDrones();

        IList<BatteryData> GetAllBatteryStats();

        Drone GetDrone(int id);

        Drone GetDrone(string serialNumber);

        Drone SaveDrone(Drone drone);
    }
}