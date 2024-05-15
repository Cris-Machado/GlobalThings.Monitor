using GlobalThings.Domain.Entities.Base;
using GlobalThings.Domain.Models;

namespace GlobalThings.Domain.Entities
{
    public class SensorEntity : EntityBase
    {
        public required string Code { get; set; }
        public required string EquipamentId { get; set; }
        public ICollection<MeasurementModel> Measurements { get; set; } = new List<MeasurementModel>();
    }
}
