using GlobalThings.Domain.Models.Base;

namespace GlobalThings.Domain.Models
{
    public class EquipamentModel : ModelBase
    {
        public string Name { get; set; }

        //public ICollection<SensorModel> Sensors { get; set; } = new List<SensorModel>();
    }
}
