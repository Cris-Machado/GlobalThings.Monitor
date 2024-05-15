using GlobalThings.IoC;

namespace GlobalThings.API.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddNativeIoC(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            RegisterAllServices(serviceCollection, configuration);
        }
        private static void RegisterAllServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            InjectorBootStrapers.RegisterServices(serviceCollection, configuration);
        }
    }
}
