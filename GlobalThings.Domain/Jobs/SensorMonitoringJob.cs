using GlobalThings.Domain.Interfaces.Jobs;
using GlobalThings.Domain.Interfaces.Repositories;
using GlobalThings.Domain.Interfaces.Services;
using GlobalThings.Domain.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Text;

namespace GlobalThings.Domain.Jobs
{
    public class SensorMonitoringJob : ISensorMonitoringJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly ILogger<SensorModel> _logger;

        public SensorMonitoringJob(IUnitOfWork unitOfWork, ILogger<SensorModel> logger, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task MonitorSensorsAsync()
        {
            try { 
            var sensors = await _unitOfWork.SensorRepository.ListAllActive();

            foreach (var sensor in sensors)
            {
                var measurements = sensor.Measurements.OrderByDescending(m => m.DateTime).Take(50).ToList();

                int consecutiveOutOfBounds = 0;
                foreach (var measurement in measurements.OrderByDescending(x => x.DateTime).Take(5))
                {
                        if (measurement.Value < 1 || measurement.Value > 50)
                            consecutiveOutOfBounds++;
                        else
                        {
                            consecutiveOutOfBounds = 0;
                            break;
                        }
                }

                if (consecutiveOutOfBounds >= 5)
                {
                    var message = new StringBuilder();
                    message.Append("Alerta!! ");
                    message.AppendLine("O sensor de código '" + sensor.Code + "' ");
                    message.Append("teve seus ultimos cinco registros de medição fora da zona segura");
                    _emailService.SendEmail(message.ToString());
                }

                var average = measurements.Average(m => m.Value);
                if (average >= -1 && average <= 3 || average >= 48 && average <= 52)
                {
                    var message = new StringBuilder();
                    message.AppendLine("Alerta!! ");
                    message.AppendLine("As medições do sensor de código '" + sensor.Code + "' estão dentro da margem de erro");
                    _emailService.SendEmail(message.ToString());
                }
            }
            }catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
