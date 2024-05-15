using GlobalThings.Domain.Configuration;
using GlobalThings.Domain.Interfaces.Repositories;
using GlobalThings.Domain.Interfaces.Services;
using GlobalThings.Domain.Services;
using GlobalThings.Repository.Context;
using GlobalThings.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalThings.IoC
{
    public class InjectorBootStrapers
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            #region ## Application
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<DbContext>();
            services.AddScoped<MonitorConfiguration>();
            #endregion

            #region ## Repositories
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISensorRepository, SensorRepository>();
            services.AddTransient<IEquipamentRepository, EquipamentRepository>();
            services.AddTransient<IMeasurementRepository, MeasurementRepository>();
            #endregion

            #region ## Services
            services.AddTransient<ISensorService, SensorService>();
            services.AddTransient<IEquipamentService, EquipamentService>();
            services.AddTransient<IMeasurementService, MeasurementService>();
            #endregion
        }
    }
}
