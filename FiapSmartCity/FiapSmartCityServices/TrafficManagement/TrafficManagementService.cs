using FiapSmartCityInfrastructure.TrafficManagement;
using FiapSmartCityServices.Log;

namespace FiapSmartCityServices.TrafficManagement
{
    public class TrafficManagementService : ITrafficManagementService
    {

        private readonly ITrafficManagementRepository _repository;
        private readonly ILogService _logService;
        private readonly Guid _sessionId;

        public TrafficManagementService(ITrafficManagementRepository repository, ILogService logService)
        {
            _repository = repository;
            _logService = logService;
            _sessionId = new Guid();
        }


    }
}
