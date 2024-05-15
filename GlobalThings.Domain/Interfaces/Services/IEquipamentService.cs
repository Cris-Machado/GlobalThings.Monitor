using GlobalThings.Domain.Models;

namespace GlobalThings.Domain.Interfaces.Services
{
    public interface IEquipamentService
    {
        Task<List<EquipamentModel>> GetAllEquipaments();
        Task<List<SensorModel>> ReportSensorByEquipament(string equipamentId);
        Task<EquipamentModel> CreateEquipament(EquipamentModel model);
        Task<EquipamentModel> UpdateEquipament(EquipamentModel model);
        Task<string> DeleteEquipament(string EquipamentId);
    }
}
