using FiapSmartCityInfrastructure.Environmental;
using FiapSmartCityServices.Log;

namespace FiapSmartCityServices.EnvironmentalMonitoring
{
    public class EnvironmentalMonitoringService : IEnvironmentalMonitoringService
    {

        private readonly IEnvironmentalMonitoringRepository _repository;
        private readonly ILogService _logService;
        private readonly Guid _sessionId;

        public EnvironmentalMonitoringService(IEnvironmentalMonitoringRepository repository, ILogService logService)
        {
            _repository = repository;
            _logService = logService;
            _sessionId = new Guid();
        }





    }
}
