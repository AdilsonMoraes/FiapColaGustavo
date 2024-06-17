using FiapSmartCityAPI.Attributes;
using FiapSmartCityDomain.Authentication;
using FiapSmartCityDomain.Authentication.ViewModel;
using FiapSmartCityServices.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapSmartCityAPI.Controllers.Authentication
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<LoginViewModel> Login([FromBody] LoginUser loginUser)
            => await _authenticationService.Login(loginUser);

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return Ok();
        }

        [ExtendedAuthorize(new string[] { "Administrador" })]
        [HttpGet]
        public async Task<ICollection<UserViewModel>> Users() 
            => await _authenticationService.Get();

        [ExtendedAuthorize(new string[] { "Administrador" })]
        [HttpDelete("{userId}")]
        public async Task<bool> DeleteUser(string userId) 
            => await _authenticationService.DeleteUser(userId);


        [ExtendedAuthorize(new string[] { "Administrador" })]
        [HttpPut]
        public async Task<UserViewModel> UpdateUser([FromBody] UserViewModel userViewModel) 
            => await _authenticationService.UpdateUser(userViewModel);


        [ExtendedAuthorize(new string[] { "Administrador" })]
        [HttpPost]
        public async Task<UserViewModel> CreateUser([FromBody] UserViewModel userViewModel) 
            => await _authenticationService.CreateUser(userViewModel);
        

        [HttpPost]
        public async Task<bool> ChangePassword([FromBody] ChangePasswordViewModel changePasswordViewModel) 
            => await _authenticationService.ChangePassword(changePasswordViewModel);
    }
}
