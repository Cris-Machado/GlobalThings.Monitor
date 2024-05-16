using GlobalThings.Domain.Interfaces.Repositories;
using GlobalThings.Domain.Interfaces.Services;
using GlobalThings.Domain.Models;
using Microsoft.Extensions.Logging;

namespace GlobalThings.Domain.Services
{
    public class SensorService : ISensorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SensorModel> _logger;

        public SensorService(IUnitOfWork unitOfWork, ILogger<SensorModel> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #region ## Searches
        public async Task<List<SensorModel>> GetAllSensors()
        {
            return await _unitOfWork.SensorRepository.ListAllActive();
        }
        #endregion

        #region ## Methods
        public async Task<SensorModel> CreateSensor(SensorModel model)
        {
            try
            {
                ValidateSensorModel(model);
                model.Id = await _unitOfWork.SensorRepository.Add(model, model.Id);
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
        public async Task<SensorModel> UpdateSensor(SensorModel model)
        {
            try
            {
                var storedSensor = await FindSensor(model.Id);
                ValidateSensorModel(model);
                await _unitOfWork.SensorRepository.Update(model, storedSensor.Id);
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
        public async Task<string> DeleteSensor(string sensorId)
        {
            try
            {
                var storedSensor = await FindSensor(sensorId);
                storedSensor.Active = false;
                await _unitOfWork.SensorRepository.Update(storedSensor, storedSensor.Id);
                return storedSensor.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
        public async Task<string> BindSensorToEquipament(string sensorId, string equipamentId)
        {
            try
            {
                var storedEquipament = await FindEquipament(equipamentId);
                var storedSensor = await FindSensor(sensorId);

                storedSensor.EquipamentId = storedEquipament.Id;
                await _unitOfWork.SensorRepository.Update(storedSensor, storedSensor.Id);
                return storedEquipament.Id;
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

        private async Task<EquipamentModel> FindEquipament(string equipamentId)
        {
            var storedSensor = await _unitOfWork.EquipamentRepository.FindById(equipamentId);
            if (storedSensor == null)
                throw new Exception("Equipamento não localizado.");

            return storedSensor;
        }

        private async void ValidateSensorModel(SensorModel sensorModel)
        {
            if (sensorModel != null)
            {
                if (string.IsNullOrEmpty(sensorModel.Code))
                    throw new Exception("Código inválido");

                var storedSensor = await _unitOfWork.SensorRepository.Where(x => x.Code == sensorModel.Code);
                if (storedSensor != null)
                    throw new Exception("Já existe um sensor com este código");

                if (!string.IsNullOrEmpty(sensorModel.EquipamentId))
                {
                    var storedEquipament = await _unitOfWork.EquipamentRepository.FindById(sensorModel.EquipamentId);
                    if (storedEquipament == null)
                        throw new Exception("Esquipamento não encontrado");
                }
            }
        }
        #endregion
    }
}
