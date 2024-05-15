using GlobalThings.Domain.Models.Base;

namespace GlobalThings.Domain.Models
{
    public class MeasurementModel : ModelBase
    {
        public DateTimeOffset DateTime { get; set; }

        public decimal Value { get; set; }
    }
}
