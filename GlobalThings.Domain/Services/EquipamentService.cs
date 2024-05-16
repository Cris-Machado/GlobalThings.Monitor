using AutoMapper;
using GlobalThings.Domain.Interfaces.Repositories;
using GlobalThings.Domain.Interfaces.Services;
using GlobalThings.Domain.Models;
using Microsoft.Extensions.Logging;

namespace GlobalThings.Domain.Services
{
    public class EquipamentService : IEquipamentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EquipamentModel> _logger;
        protected readonly IMapper _mapper;

        public EquipamentService(IUnitOfWork unitOfWork, ILogger<EquipamentModel> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        #region ## Searches
        public async Task<List<EquipamentModel>> GetAllEquipaments()
        {
            return await _unitOfWork.EquipamentRepository.ListAllActive();
        }

        public async Task<List<SensorModel>> ReportSensorByEquipament(string equipamentId)
        {
            var storedEquipament = await FindEquipament(equipamentId);
            var storedSensors = await _unitOfWork.SensorRepository.WhereList(x => x.EquipamentId == equipamentId);

            foreach (var item in storedSensors)
                item.Measurements.OrderByDescending(x => x.DateTime).Take(10).ToList();

            return storedSensors;
        }
        #endregion

        #region ## Methods
        public async Task<EquipamentModel> CreateEquipament(EquipamentModel model)
        {
            try
            {
                ValidateEquipamentModel(model);
                model.Id = await _unitOfWork.EquipamentRepository.Add(model, model.Id);
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
        public async Task<EquipamentModel> UpdateEquipament(EquipamentModel model)
        {
            try
            {
                var storedEquipament = await FindEquipament(model.Id);
                ValidateEquipamentModel(model);
                await _unitOfWork.EquipamentRepository.Update(model, storedEquipament.Id);
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw new Exception(ex.ToString());
            }
        }
        public async Task<string> DeleteEquipament(string equipamentId)
        {
            try
            {
                var storedEquipament = await FindEquipament(equipamentId);
                storedEquipament.Active = false;
                await _unitOfWork.EquipamentRepository.Update(storedEquipament, storedEquipament.Id);
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
        private async Task<EquipamentModel> FindEquipament(string equipamentId)
        {
            var storedEquipament = await _unitOfWork.EquipamentRepository.FindById(equipamentId);
            if (storedEquipament == null)
                throw new Exception("Equipamento não localizado.");

            return storedEquipament;
        }

        private async Task<SensorModel> FindSensor(string sensorId)
        {
            var storedSensor = await _unitOfWork.SensorRepository.FindById(sensorId);
            if (storedSensor == null)
                throw new Exception("Sensor não localizado.");

            return storedSensor;
        }

        private void ValidateEquipamentModel(EquipamentModel EquipamentModel)
        {
            if (EquipamentModel != null)
            {
                if (string.IsNullOrEmpty(EquipamentModel.Name))
                    throw new Exception("O Nome é obrigatório");
            }
        }
        #endregion
    }
}
