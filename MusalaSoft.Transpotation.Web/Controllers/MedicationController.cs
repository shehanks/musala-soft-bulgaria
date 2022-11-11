using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusalaSoft.Transpotation.BusinessServices.Contract;
using MusalaSoft.Transpotation.Domain.Enums;
using MusalaSoft.Transpotation.Domain.Models;
using MusalaSoft.Transpotation.Domain.RequestModels;
using MusalaSoft.Transpotation.Helpers;
using System.Text.RegularExpressions;

namespace MusalaSoft.Transpotation.Web.Controllers
{
    /// <summary>
    /// The medication API endpoint.
    /// </summary>
    [Route("api/medication")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        #region Fields

        private readonly IDroneService _droneService;
        private readonly IMedicationDispatchService _medicationDispatchService;
        private readonly ILogger<MedicationController> _logger;
        private readonly IMapper _mapper;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// The medication controller constructor.
        /// </summary>
        /// <param name="droneService">The drone service.</param>
        /// <param name="medicationDispatchService">The medication dispatch service.</param>
        /// <param name="logger">The logger use to add logs in event log.</param>
        /// <param name="mapper">The automapper to map entities.</param>
        public MedicationController(IDroneService droneService,
            IMedicationDispatchService medicationDispatchService,
            ILogger<MedicationController> logger,
            IMapper mapper)
        {
            _droneService = droneService;
            _medicationDispatchService = medicationDispatchService;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion Constructor

        #region API Endpoints.

        /// <summary>
        /// This API is used to load medications to a drone.
        /// In this stage the drone state is LOADING.
        /// </summary>
        /// <param name="request">The json result.</param>
        /// <returns></returns>
        [Route("load")]
        [HttpPost]
        public JsonResult LoadMedication(LoadMedicationRequest request)
        {
            var droneId = request.droneId;
            var medicationItems = request.medicationItems;

            try
            {
                var drone = _droneService.GetDrone(droneId);

                if (drone == null)
                    return new JsonResult(NotFound("Drone not found."));
                else if (drone.StateId != (int)DroneStateType.IDLE)
                    return new JsonResult(BadRequest($"Drone is not available. Current state is {(DroneStateType)drone.StateId}"));

                var validationContext = IsValidMedication(drone, medicationItems);
                if (validationContext.Item1)
                {
                    var medications = _mapper.Map<List<Medication>>(medicationItems);
                    var tripId = _medicationDispatchService.LoadMedications(drone, medications);
                    return new JsonResult(Ok($"Medications are loading to drone: {droneId}, trip Id: {tripId}, state: {DroneStateType.LOADING}"));
                }

                return new JsonResult(BadRequest($"{validationContext.Item2}"));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, e.Message));
            }
        }

        /// <summary>
        /// This API is used to fetch the LOADED medications for a given drone.
        /// </summary>
        /// <param name="Id">The drone id.</param>
        /// <returns>The json result.</returns>
        [Route("loaded")]
        [HttpPost]
        public JsonResult GetLoadedMedications(IdRequest request)
        {
            try
            {
                var drone = _droneService.GetDrone(request.Id);

                if (drone == null)
                    return new JsonResult(NotFound("Drone not found."));
                else if (drone.StateId != (int)DroneStateType.LOADED)
                    return new JsonResult(BadRequest($"Drone is not in LOADED state. Current state is {(DroneStateType)drone.StateId}"));


                var loadedMedications = _medicationDispatchService.GetLoadedMedications(drone);
                return new JsonResult(Ok(loadedMedications));

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, e.Message));
            }
        }

        #endregion API Endpoints

        #region Support Methods

        /// <summary>
        /// Validate medications data and it's entities.
        /// </summary>
        /// <param name="drone">The drone entity.</param>
        /// <param name="medications">The list of medication entity.</param>
        /// <returns>A tuple, whether all the medications valid and status message.</returns>
        private Tuple<bool, string> IsValidMedication(Drone drone, IList<MedicationRequest> medications)
        {
            decimal totalWeight = 0;
            var maxWeightLimit = DroneHelpers.GetMaxWeightLimit(drone.ModelId);

            if (drone.BatteryPercentage < 25)
            {
                return Tuple.Create(false, $"Battery percentage is {drone.BatteryPercentage}%. 25% is requred minimum.");
            }
            else if (medications != null && medications.Any())
            {
                foreach (var me in medications)
                {
                    if (me.Weight <= 0)
                        return Tuple.Create(false, $"Weight is required.");
                    else if (!IsValidName(me.Name) || !IsValidCode(me.Code))
                        return Tuple.Create(false, $"Invalid medication - Please check the Name: {me.Name} and Code: {me.Code} formats.");

                    totalWeight += me.Weight;

                    if (totalWeight > maxWeightLimit)
                        return Tuple.Create(false, $"Weight limit is over than max: {maxWeightLimit}");

                }
            }

            return Tuple.Create(true, string.Empty);
        }

        /// <summary>
        /// Validate code format of a medication.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>Whether the code is valid or not.</returns>
        private bool IsValidCode(string code)
        {
            var codeRegex = @"^[A-Z0-9_]*$";
            if (!code.IsNullOrEmpty() && Regex.IsMatch(code, codeRegex))
                return true;

            return false;
        }

        /// <summary>
        /// Validate name format of a medication.
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns>Whether the name is valid or not.</returns>
        private bool IsValidName(string name)
        {
            var nameRegex = @"^[a-zA-Z0-9_-]*$";
            if (!name.IsNullOrEmpty() && Regex.IsMatch(name, nameRegex))
                return true;

            return false;
        }

        #endregion Support Methods
    }
}
