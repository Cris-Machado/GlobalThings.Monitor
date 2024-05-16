using GlobalThings.Domain.Entities.Base;
namespace GlobalThings.Domain.Entities
{
    public class MeasurementDto
    {
        public required decimal Value { get; set; }
        public required string SensorId { get; set; }
    }
}
