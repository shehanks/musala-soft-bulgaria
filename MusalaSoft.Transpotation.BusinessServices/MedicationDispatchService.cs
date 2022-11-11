using MusalaSoft.Transpotation.BusinessServices.Contract;
using MusalaSoft.Transpotation.Domain.Enums;
using MusalaSoft.Transpotation.Domain.Models;
using MusalaSoft.Transpotation.Repository.Contract;

namespace MusalaSoft.Transpotation.BusinessServices
{
    public class MedicationDispatchService : IMedicationDispatchService
    {
        private readonly IDroneDao _droneDao;
        private readonly IMedicationDao _medicationDao;
        private readonly IDroneTripDao _droneTripDao;

        public MedicationDispatchService(
            IDroneDao droneDao,
            IMedicationDao medicationDao,
            IDroneTripDao droneTripDao)
        {
            _droneDao = droneDao;
            _medicationDao = medicationDao;
            _droneTripDao = droneTripDao;
        }

        public IList<Medication> GetLoadedMedications(Drone drone)
        {
            var currentTripId = drone!.CurrentTripId;
            var medicationList = _medicationDao.GetTripMedications(currentTripId).ToList();

            return medicationList;
        }

        public int LoadMedications(Drone drone, IList<Medication> medications)
        {
            // Save new drone trip.
            var newDroneTrip = _droneTripDao.SaveDroneTrip(droneTrip: new DroneTrip() { DroneId = drone.Id });

            // Update drone state(LOADING) and trip Id.
            drone.CurrentTripId = newDroneTrip.Id;
            drone.StateId = (int)DroneStateType.LOADING;
            _droneDao.SaveDrone(drone);

            // Loading/Adding medications to the drone.
            var added = _medicationDao.SaveMedications(newDroneTrip.Id, medications);

            if (added)
                return newDroneTrip.Id;

            return 0;
        }
    }
}
