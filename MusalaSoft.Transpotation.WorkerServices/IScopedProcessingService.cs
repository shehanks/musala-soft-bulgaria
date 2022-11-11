namespace MusalaSoft.Transpotation.WorkerServices
{
    public interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}