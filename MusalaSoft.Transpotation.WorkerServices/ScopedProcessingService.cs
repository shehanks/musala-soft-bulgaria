using Microsoft.Extensions.Logging;
using MusalaSoft.Transpotation.BusinessServices.Contract;
using System.Text;

namespace MusalaSoft.Transpotation.WorkerServices
{
    public class ScopedProcessingService : IScopedProcessingService
    {
        private int executionCount = 0;
        private int delay = 60000;
        private readonly ILogger _logger;
        private readonly IDroneService _droneService;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger, IDroneService droneService)
        {
            _logger = logger;
            _droneService = droneService;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;

                _logger.LogInformation(
                    "Scoped Processing Service is working. Count: {Count}", executionCount);
                try
                {
                    var statList = _droneService.GetAllBatteryStats();

                    if (statList.Any())
                    {
                        StringBuilder builder = new StringBuilder();
                        foreach (var stat in statList)
                        {
                            builder.Append($"Drone ({stat.DroneId}) - {stat.DroneSerialNumber} => Battery percentage -  {stat.Percentage}\r\n");
                        }

                        _logger.LogInformation(builder.ToString());
                    }
                    else
                    {
                        _logger.LogInformation("There are no registered drones at the moment.");
                    }
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"Unable to fetch data {DateTimeOffset.Now} \r\n Exception: {e.Message}");
                }

                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}
