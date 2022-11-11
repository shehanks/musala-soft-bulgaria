using MusalaSoft.Transpotation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusalaSoft.Transpotation.Repository.Contract
{
    public interface IDroneDao : IRepository<DroneDataModel>
    {
        IList<Drone> GetAllDrones();

        IList<Drone> GetAvailableDrones();

        Drone GetDrone(int id);

        Drone GetDrone(string serialNumber);

        Drone SaveDrone(Drone drone);

        IList<BatteryData> GetAllBatteryData();
    }
}
