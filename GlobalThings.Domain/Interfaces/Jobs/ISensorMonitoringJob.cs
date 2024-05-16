namespace GlobalThings.Domain.Interfaces.Jobs
{
    public interface ISensorMonitoringJob
    {
        Task MonitorSensorsAsync();
    }
}
