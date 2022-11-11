using Microsoft.Extensions.Logging;
using MusalaSoft.Transpotation.BusinessServices.Contract;
using MusalaSoft.Transpotation.Domain.Enums;
using MusalaSoft.Transpotation.Domain.Models;
using MusalaSoft.Transpotation.Repository.Contract;

namespace MusalaSoft.Transpotation.BusinessServices
{
    public class DroneService : IDroneService
    {
        private readonly IDroneDao _droneDao;

        public DroneService(IDroneDao droneDao)
        {
            _droneDao = droneDao;
        }

        public IList<BatteryData> GetAllBatteryStats()
        {
            return _droneDao.GetAllBatteryData();
        }

        public IList<Drone> GetAllDrones()
        {
            return _droneDao.GetAllDrones().ToList();
        }

        public IList<Drone> GetAvailableDrones()
        {
            return _droneDao.GetAvailableDrones();
        }

        public Drone GetDrone(int id)
        {
            return _droneDao.GetDrone(id);
        }

        public Drone GetDrone(string serialNumber)
        {
            return _droneDao.GetDrone(serialNumber);
        }

        public Drone SaveDrone(Drone drone)
        {
            return _droneDao.SaveDrone(drone);
        }
    }
}