using FiapSmartCityDomain.Authentication;
using FiapSmartCityDomain.Authentication.ViewModel;
using FiapSmartCityDomain.Enums;
using FiapSmartCityInfrastructure.Authentication;
using FiapSmartCityServices.Log;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FiapSmartCityServices.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ILogService _logService;
        private readonly Guid _sessionId;

        public AuthenticationService(IAuthenticationRepository authenticationRepository, ILogService logService)
        {
            _authenticationRepository = authenticationRepository;
            _logService = logService;
            _sessionId = new Guid();
        }
       
        public async Task<bool> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                var result = await _authenticationRepository.ChangePassword(changePasswordViewModel);
                _logService.LogMessage("End", _sessionId, LogType.Info);

                return result;
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, _sessionId);
                throw;
            }
        }
        public async Task<UserViewModel> CreateUser(UserViewModel userViewModel)
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                var result = await _authenticationRepository.CreateUser(userViewModel);
                _logService.LogMessage("End", _sessionId, LogType.Info);

                return result;
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, _sessionId);
                throw;
            }
        }
        public async Task<bool> DeleteUser(string userId)
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                var result = await _authenticationRepository.DeleteUser(userId);
                _logService.LogMessage("End", _sessionId, LogType.Info);

                return result;
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, _sessionId);
                throw;
            }
        }
        public async Task<ICollection<UserViewModel>> Get()
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                var result = await _authenticationRepository.Get();
                _logService.LogMessage("End", _sessionId, LogType.Info);

                return result;
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, _sessionId);
                throw;
            }
        }
        public async Task<ICollection<RoleViewModel>> GetRoles()
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                var result = await _authenticationRepository.GetRoles();
                _logService.LogMessage("End", _sessionId, LogType.Info);

                return result;
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, _sessionId);
                throw;
            }
        }
        public async Task<User> GetUser()
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                var result = await _authenticationRepository.GetUser();
                _logService.LogMessage("End", _sessionId, LogType.Info);

                return result;
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, _sessionId);
                throw;
            }
        }
        public async Task<UserViewModel> GetUserWithRole()
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                var result = await _authenticationRepository.GetUserWithRole();
                _logService.LogMessage("End", _sessionId, LogType.Info);

                return result;
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, _sessionId);
                throw;
            }
        }
        public async Task<bool> IsAdmin()
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                var result = await _authenticationRepository.IsAdmin();
                _logService.LogMessage("End", _sessionId, LogType.Info);

                return result;
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, _sessionId);
                throw;
            }
        }
        public async Task<LoginViewModel> Login(LoginUser loginUser)
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                var result = await _authenticationRepository.Login(loginUser);
                _logService.LogMessage("End", _sessionId, LogType.Info);

                return result;
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, _sessionId);
                throw;
            }
        }
        public async Task Logout()
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                await _authenticationRepository.Logout();
                _logService.LogMessage("End", _sessionId, LogType.Info);
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, _sessionId);
                throw;
            }
        }
        public async Task<UserViewModel> UpdateUser(UserViewModel userViewModel)
        {
            try
            {
                _logService.LogMessage("Begin", _sessionId, LogType.Info);
                var result = await _authenticationRepository.UpdateUser(userViewModel);
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
