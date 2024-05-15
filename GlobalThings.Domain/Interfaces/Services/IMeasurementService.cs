using GlobalThings.Domain.Models;

namespace GlobalThings.Domain.Interfaces.Services
{
    public interface IMeasurementService
    {
        Task<List<MeasurementModel>> GetAllMeasurements();
        Task<MeasurementModel> CreateMeasurement(MeasurementModel model, string sensorId);
    }
}
