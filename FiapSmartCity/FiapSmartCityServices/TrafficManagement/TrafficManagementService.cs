using FiapSmartCityDomain.Authentication.ViewModel;
using FiapSmartCityDomain.Enums;
using FiapSmartCityDomain.TrafficManagement.ViewModel;
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

        public async Task<AccidentsViewModel> CreateAccidents(AccidentsViewModel accidentsViewModel)
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                var result = await _repository.CreateAccidents(accidentsViewModel);
                _logService.LogMessage("End", _sessionId, LogType.Info);

                return result;
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, _sessionId);
                throw;
            }
        }

        public async Task<ICollection<AccidentsViewModel>> GetAccidents()
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                var result = await _repository.GetAccidents();
                _logService.LogMessage("End", _sessionId, LogType.Info);

                return result;
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, _sessionId);
                throw;
            }
        }
    }
}
