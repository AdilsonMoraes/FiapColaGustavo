using FiapSmartCityDomain.Authentication;
using FiapSmartCityDomain.Authentication.ViewModel;

namespace FiapSmartCityServices.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginViewModel> Login(LoginUser loginUser);
        Task<User> GetUser();
        Task Logout();
        Task<ICollection<UserViewModel>> Get();
        Task<bool> DeleteUser(string userId);
        Task<ICollection<RoleViewModel>> GetRoles();
        Task<UserViewModel> UpdateUser(UserViewModel userViewModel);
        Task<UserViewModel> CreateUser(UserViewModel userViewModel);
        Task<bool> ChangePassword(ChangePasswordViewModel changePasswordViewModel);
        Task<UserViewModel> GetUserWithRole();
        Task<bool> IsAdmin();
    }
}
