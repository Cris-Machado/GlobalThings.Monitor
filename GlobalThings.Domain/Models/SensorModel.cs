using GlobalThings.Domain.Models.Base;
using System.Text.Json.Serialization;

namespace GlobalThings.Domain.Models
{
    public class SensorModel : ModelBase
    {
        [JsonRequired]
        public string Code { get; set; }
        public string EquipamentId { get; set; }
        public ICollection<MeasurementModel> Measurements { get; set; } = new List<MeasurementModel>();
    }
}
