using AutoMapper;
using MusalaSoft.Transpotation.DataAccess;
using MusalaSoft.Transpotation.Domain.Enums;
using MusalaSoft.Transpotation.Domain.Models;
using MusalaSoft.Transpotation.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusalaSoft.Transpotation.Repository
{
    public class DroneDao : Repository<DroneDataModel>, IDroneDao
    {
        private readonly IMapper _mapper;

        public DroneDao(ApplicationDBContext applicationDbContext, IMapper mapper)
            : base(applicationDbContext)
        {
            _mapper = mapper;
        }

        public IList<Drone> GetAllDrones()
        {
            return _mapper.Map<List<Drone>>(GetAll());
        }

        public IList<Drone> GetAvailableDrones()
        {
            return _mapper.Map<List<Drone>>(FindList(e => e.StateId == (int)DroneStateType.IDLE));
        }

        public Drone GetDrone(int id)
        {
            return _mapper.Map<Drone>(Find(e => e.Id == id));
        }

        public Drone GetDrone(string serialNumber)
        {
            return _mapper.Map<Drone>(Find(e => e.SerialNumber.Equals(serialNumber)));
        }

        public Drone SaveDrone(Drone drone)
        {
            using (var context = new ApplicationDBContext())
            {
                var dataModel = _mapper.Map<DroneDataModel>(drone);

                if (drone.Id > 0)
                    context.Drone.Update(dataModel);
                else
                    context.Drone.Add(dataModel);

                context.SaveChanges();
                drone.Id = dataModel.Id;
            }

            return drone;
        }

        public IList<BatteryData> GetAllBatteryData()
        {
            using (var context = new ApplicationDBContext())
            {
                var batteryData = context.Drone.Select(x => new BatteryData() 
                { 
                    DroneId = x.Id,
                    DroneSerialNumber = x.SerialNumber,
                    Percentage = x.BatteryPercentage
                }).ToList();

                return batteryData;
            }
        }
    }
}
