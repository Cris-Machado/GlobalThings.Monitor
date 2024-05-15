using GlobalThings.Domain.Models;

namespace GlobalThings.Domain.Interfaces.Services
{
    public interface ISensorService
    {
        Task<List<SensorModel>> GetAllSensors();
        Task<SensorModel> CreateSensor(SensorModel model);
        Task<SensorModel> UpdateSensor(SensorModel model);
        Task<string> DeleteSensor(string sensorId);
        Task<string> BindSensorToEquipament(string sensorId, string equipamentId);
    }
}
