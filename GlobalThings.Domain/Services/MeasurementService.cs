using GlobalThings.Domain.Interfaces.Repositories;
using GlobalThings.Domain.Interfaces.Services;
using GlobalThings.Domain.Models;
using Microsoft.Extensions.Logging;

namespace GlobalThings.Domain.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MeasurementModel> _logger;

        public MeasurementService(IUnitOfWork unitOfWork, ILogger<MeasurementModel> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #region ## Searches
        public async Task<List<MeasurementModel>> GetAllMeasurements()
        {
            return await _unitOfWork.MeasurementRepository.ListAllActive();
        }
        #endregion

        #region ## Methods
        public async Task<MeasurementModel> CreateMeasurement(MeasurementModel model, string sensorId)
        {
            try
            {
                model.Id = await _unitOfWork.MeasurementRepository.Add(model, model.Id);
                UpdateSensorMeasurements(model, sensorId);

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
        #endregion

        #region ## Private Methods
        private async Task<SensorModel> FindSensor(string sensorId)
        {
            var storedSensor = await _unitOfWork.SensorRepository.FindById(sensorId);
            if (storedSensor == null)
                throw new Exception("Sensor não localizado.");

            return storedSensor;
        }

        private async void UpdateSensorMeasurements(MeasurementModel model, string sensorId)
        {
            var storedSensor = await FindSensor(sensorId);
            storedSensor.Measurements.Add(model);

            await _unitOfWork.SensorRepository.Update(storedSensor, sensorId);
        }
        #endregion
    }
}
