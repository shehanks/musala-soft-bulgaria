using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusalaSoft.Transpotation.BusinessServices.Contract;
using MusalaSoft.Transpotation.Domain.Enums;
using MusalaSoft.Transpotation.Domain.Models;
using MusalaSoft.Transpotation.Domain.RequestModels;
using Newtonsoft.Json;

namespace MusalaSoft.Transpotation.Web.Controllers
{
    /// <summary>
    /// The drone API endpoint.
    /// </summary>
    [Route("api/drone")]
    [ApiController]
    public class DroneController : ControllerBase
    {
        #region Fields

        private readonly IDroneService _droneService;
        private readonly IMapper _mapper;
        private readonly ILogger<DroneController> _logger;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// The drone controler constructor.
        /// </summary>
        /// <param name="droneService">Drone service.</param>
        /// <param name="mapper">The automapper to map entities.</param>
        /// <param name="logger">The logger use to add logs in event log.</param>
        public DroneController(IDroneService droneService, IMapper mapper, ILogger<DroneController> logger)
        {
            _droneService = droneService;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion Constructor

        #region API Endpoints

        /*
        [Route("get")]
        [HttpGet]
        public JsonResult GetDroneById(int id)
        {
            try
            {
                var drone = _droneService.GetDrone(id);

                if (drone == null)
                    return new JsonResult(NotFound("Drone not found."));

                return new JsonResult(Ok(drone));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, e.Message));
            }    
        }

        [Route("all")]
        [HttpGet]
        public JsonResult GetAllDrones()
        {
            try
            {
                var drones = _droneService.GetAllDrones();

                if (!drones.Any())
                    return new JsonResult(NotFound("Drone not found."));

                return new JsonResult(Ok(drones));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, e.Message));
            }  
        }
        */

        /// <summary>
        /// This API is used to register a drone.
        /// Drone state must be always IDLE for a new registration.
        /// </summary>
        /// <param name="droneRequest">The registration request data for a drone.</param>
        /// <returns>The json result.</returns>
        [Route("register")]
        [HttpPost]
        public JsonResult SaveDrone(DroneRegisterRequest droneRequest)
        {
            try
            {
                var drone = _mapper.Map<Drone>(droneRequest);

                if (IsValidDrone(drone))
                {
                    drone.StateId = (int)DroneStateType.IDLE;
                    var saved = _droneService.SaveDrone(drone);

                    if (saved != null)
                        return new JsonResult(Ok(saved));
                    else
                        return new JsonResult(NotFound("Drone not saved."));
                }

                return new JsonResult(BadRequest("Please check the request."));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, e.Message));
            }

        }

        /// <summary>
        /// This API is used to fetch the available drones.
        /// That means the drones with IDLE state.
        /// </summary>
        /// <returns>The json result.</returns>
        [Route("available")]
        [HttpGet]
        public JsonResult GetAvailableDrones()
        {
            try
            {
                var drones = _droneService.GetAvailableDrones();

                if (!drones.Any())
                    return new JsonResult(NotFound("There are no available drones at the moment."));

                return new JsonResult(Ok(drones));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, e.Message));
            }
        }

        /// <summary>
        /// This API is used to fetch the battery percentage of a given drone.
        /// </summary>
        /// <param name="id">The drone Id.</param>
        /// <returns>The json result.</returns>
        [Route("capacity")]
        [HttpPost]
        public JsonResult GetDroneBatteryPercentage(IdRequest request)
        {
            try
            {
                var drone = _droneService.GetDrone(request.Id);

                if (drone == null)
                    return new JsonResult(NotFound("Drone not found."));

                return new JsonResult(Ok(new { Percentage = drone.BatteryPercentage }));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, e.Message));
            }
        }

        /// <summary>
        /// This method is used to change the state of a drone.
        /// </summary>
        /// <param name="request">The request data.</param>
        /// <returns>The json result.</returns>
        [Route("changestate")]
        [HttpPost]
        public JsonResult ChangeDroneState(DroneStateChangeRequest request)
        {
            try
            {
                var drone = _droneService.GetDrone(request.DroneId);

                if (drone == null)
                    return new JsonResult(NotFound("Drone not found."));
                if (!drone.CurrentTripId.HasValue && request.StateId != (int)DroneStateType.IDLE)
                    return new JsonResult(BadRequest("This drone is not on a trip at the moment. State must be IDLE."));

                if (request.StateId == (int)DroneStateType.IDLE)
                    drone.CurrentTripId = null;

                drone.StateId = request.StateId;
                var updated = _droneService.SaveDrone(drone);
                return new JsonResult(Ok(updated));

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, e.Message));
            }

        }

        /*
        [Route("capacity")]
        [HttpGet]
        public JsonResult GetDroneBatteryPercentage(string serialNumber)
        {
            try
            {
                var drone = _droneService.GetDrone(serialNumber);

                if (drone == null || drone.Id == 0)
                    return new JsonResult(NotFound("Drone not found."));

                return new JsonResult(Ok(new { Percentage = drone.BatteryPercentage }));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new JsonResult(StatusCode(StatusCodes.Status500InternalServerError, e.Message));
            }
        }
        */

        #endregion API Endpoints

        #region Support Methods

        /// <summary>
        /// Determine the validity of a drone data.
        /// </summary>
        /// <param name="drone">Whether valid or not.</param>
        /// <returns></returns>
        private bool IsValidDrone(Drone drone)
        {
            if (Enum.IsDefined(typeof(DroneModelType), drone.ModelId) &&
                !drone.SerialNumber.IsNullOrEmpty() &&
                 drone.BatteryPercentage >= 0 && drone.BatteryPercentage <= 100)
                return true;

            return false;
        }

        #endregion Support Methods
    }
}
