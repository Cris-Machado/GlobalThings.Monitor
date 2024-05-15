using GlobalThings.Domain.Entities.Base;
namespace GlobalThings.Domain.Entities
{
    public class MeasurementEntity : EntityBase
    {
        public required DateTimeOffset DateTime { get; set; }

        public required decimal Value { get; set; }
    }
}
