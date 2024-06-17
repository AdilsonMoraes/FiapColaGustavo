using FiapSmartCityInfrastructure.WasteManagement;
using FiapSmartCityServices.Log;

namespace FiapSmartCityServices.WasteManagement
{
    public class WasteManagementService : IWasteManagementService
    {

        private readonly IWasteManagementRepository _repository;
        private readonly ILogService _logService;
        private readonly Guid _sessionId;

        public WasteManagementService(IWasteManagementRepository repository, ILogService logService)
        {
            _repository = repository;
            _logService = logService;
            _sessionId = new Guid();
        }




    }
}
