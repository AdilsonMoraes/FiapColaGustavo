using FiapSmartCityInfrastructure.PublicSecurity;
using FiapSmartCityServices.Log;

namespace FiapSmartCityServices.PublicSecurity
{
    public class PublicSecurityService : IPublicSecurityService
    {

        private readonly IPublicSecurityRepository _repository;
        private readonly ILogService _logService;
        private readonly Guid _sessionId;

        public PublicSecurityService(IPublicSecurityRepository repository, ILogService logService)
        {
            _repository = repository;
            _logService = logService;
            _sessionId = new Guid();
        }


    }
}
