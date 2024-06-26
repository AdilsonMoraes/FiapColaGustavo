using FiapSmartCityInfrastructure;
using FiapSmartCityInfrastructure.Authentication;
using FiapSmartCityInfrastructure.Environmental;
using FiapSmartCityInfrastructure.PublicSecurity;
using FiapSmartCityInfrastructure.TrafficManagement;
using FiapSmartCityInfrastructure.WasteManagement;
using FiapSmartCityServices.Authentication;
using FiapSmartCityServices.Log;
using FiapSmartCityServices.TrafficManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FiapSmartCityIoC
{
    public static class DependencyResolver
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddTransient<IWasteManagementService, WasteManagementService>();
            services.AddTransient<ITrafficManagementService, TrafficManagementService>();
            //services.AddTransient<IEnvironmentalMonitoringService, EnvironmentalMonitoringService>();
            //services.AddTransient<IPublicSecurityService, PublicSecurityService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<ILogService, LogService>();
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddTransient<IWasteManagementRepository, WasteManagementRepository>();
            services.AddTransient<ITrafficManagementRepository, TrafficManagementRepository>();
            services.AddTransient<IEnvironmentalMonitoringRepository, EnvironmentalMonitoringRepository>();
            services.AddTransient<IPublicSecurityRepository, PublicSecurityRepository>();
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
        }

        public static void RegisterDB(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SmartCitiesContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}
