using GlobalThings.Domain.Interfaces.Repositories;
using GlobalThings.Domain.Interfaces.Services;
using GlobalThings.Domain.Jobs;
using GlobalThings.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace YourNamespace.Tests
{
    public class SensorMonitoringJobTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly Mock<ILogger<SensorModel>> _loggerMock;
        private readonly SensorMonitoringJob _service;

        public SensorMonitoringJobTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _emailServiceMock = new Mock<IEmailService>();
            _loggerMock = new Mock<ILogger<SensorModel>>();
            _service = new SensorMonitoringJob(_unitOfWorkMock.Object, _loggerMock.Object, _emailServiceMock.Object);
        }

        private SensorModel CreateSensorWithMeasurements(List<MeasurementModel> measurements)
        {
            return new SensorModel { Measurements = measurements };
        }

        [Fact]
        public async Task MonitorSensorsAsync_ShouldSendEmail_WhenMeasurementsAreOutOfBounds()
        {
            // Arrange
            var sensor = CreateSensorWithMeasurements(new List<MeasurementModel>
            {
                new MeasurementModel { Value = 0.5m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 0.8m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 0.6m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 0.9m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 0.4m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 2.0m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 3.0m, DateTime = DateTime.UtcNow }
            });

            _unitOfWorkMock.Setup(u => u.SensorRepository.ListAllActive()).ReturnsAsync(new List<SensorModel> { sensor });

            await _service.MonitorSensorsAsync();

            _emailServiceMock.Verify(e => e.SendEmail(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task MonitorSensorsAsync_ShouldSendEmail_WhenAverageIsWithinErrorMargin()
        {
            // Arrange
            var sensor = CreateSensorWithMeasurements(new List<MeasurementModel>
            {
                new MeasurementModel { Value = 49.5m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 49.7m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 49.6m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 49.8m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 49.4m, DateTime = DateTime.UtcNow }
            });

            _unitOfWorkMock.Setup(u => u.SensorRepository.ListAllActive()).ReturnsAsync(new List<SensorModel> { sensor });

            await _service.MonitorSensorsAsync();

            _emailServiceMock.Verify(e => e.SendEmail(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task MonitorSensorsAsync_ShouldNotSendEmail_WhenNoConditionIsMet()
        {
            // Arrange
            var sensor = CreateSensorWithMeasurements(new List<MeasurementModel>
            {
                new MeasurementModel { Value = 25.0m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 25.1m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 24.9m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 24.8m, DateTime = DateTime.UtcNow },
                new MeasurementModel { Value = 25.2m, DateTime = DateTime.UtcNow }
            });

            _unitOfWorkMock.Setup(u => u.SensorRepository.ListAllActive()).ReturnsAsync(new List<SensorModel> { sensor });

            // Act
            await _service.MonitorSensorsAsync();

            // Assert
            _emailServiceMock.Verify(e => e.SendEmail(It.IsAny<string>()), Times.Never);
        }
    }
}
