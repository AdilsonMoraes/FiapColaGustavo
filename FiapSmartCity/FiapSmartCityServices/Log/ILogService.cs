using FiapSmartCityDomain.Enums;

namespace FiapSmartCityServices.Log
{
    public interface ILogService
    {
        void LogMessage(string message, Guid sessionGuid, LogType logType, string applicationName = "SmartCitiesWebApi");

        void LogException(Exception ex, Guid sessionGuid, string applicationName = "SmartCitiesWebApi");
    }
}
