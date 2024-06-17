using AutoMapper;
using FiapSmartCityDomain.Authentication;
using FiapSmartCityDomain.Authentication.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FiapSmartCityInfrastructure.Authentication
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SmartCitiesContext _context;

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;


        public AuthenticationRepository(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, SignInManager<User> signInManager, SmartCitiesContext context,
            IConfiguration configuration, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;

            _configuration = configuration;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginViewModel> Login(LoginUser loginUser)
        {
            var result = _signInManager.PasswordSignInAsync(loginUser.UserName, loginUser.Password, false, false).Result;
            if (result.Succeeded)
            {
                var appUser = _userManager.Users.FirstOrDefault(u => u.UserName == loginUser.UserName && u.Active);
                if (appUser != null)
                {
                    var token = GenerateJwtToken(appUser);
                    var loginViewModel = _mapper.Map<LoginViewModel>(appUser);
                    loginViewModel.Token = token;
                    loginViewModel.Roles = await _userManager.GetRolesAsync(appUser);
                    return loginViewModel;

                }
            }
            return null;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(Sectoken);
        }

        public async Task<User> GetUser()
            => await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        public async Task Logout()
            => await _signInManager.SignOutAsync();

        public async Task<ICollection<UserViewModel>> Get()
        {
            var userEntities = await _userManager.Users.ToListAsync();
            var result = _mapper.Map<ICollection<UserViewModel>>(userEntities);
            result.ToList().ForEach(u =>
            {
                var userEntity = userEntities.FirstOrDefault(ue => ue.Id == u.Id);
                if (userEntity != null)
                {
                    u.Roles = _userManager.GetRolesAsync(userEntity)?.Result;
                }
            });
            return result;
        }

        public async Task<bool> DeleteUser(string userId)
        {

            var result = false;
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                await DeleteRolesFromUser(user);
                result = _userManager.DeleteAsync(user)?.Result?.Succeeded ?? false;
            }
            return result;

        }

        private async Task DeleteRolesFromUser(User userEntity)
        {
            var initialRoles = await _userManager.GetRolesAsync(userEntity);
            if (initialRoles?.Any() ?? false)
            {
                initialRoles.ToList().ForEach(ir =>
                {
                    var roleRemoveResult = _userManager.RemoveFromRoleAsync(userEntity, ir)?.Result;
                });
            }
        }

        public async Task<ICollection<RoleViewModel>> GetRoles()
            => _mapper.Map<ICollection<RoleViewModel>>(await _roleManager.Roles.ToListAsync());

        public async Task<UserViewModel> UpdateUser(UserViewModel userViewModel)
        {
            var result = userViewModel;
            var userEntity = _context.Users.FirstOrDefault(u => u.Id == userViewModel.Id);
            if (userEntity != null)
            {
                userEntity = _mapper.Map(userViewModel, userEntity);
                if (_userManager.UpdateAsync(userEntity)?.Result?.Succeeded ?? false)
                {
                    await DeleteRolesFromUser(userEntity);
                    await AddRolesToUser(userViewModel, userEntity);
                    result = _mapper.Map<UserViewModel>(userEntity);
                }
            }
            return result;
        }

        private async Task AddRolesToUser(UserViewModel userViewModel, User userEntity)
        {
            if (userViewModel?.Roles?.Any() ?? false)
            {
                userViewModel?.Roles?.ToList().ForEach(r =>
                {
                    var roleAddResult = _userManager.AddToRoleAsync(userEntity, r).Result;
                });
            }
        }

        public async Task<UserViewModel> CreateUser(UserViewModel userViewModel)
        {
            var result = userViewModel;
            var userEntity = _mapper.Map<User>(userViewModel);
            userEntity.Id = Guid.NewGuid().ToString();
            var resultAdd = _userManager.CreateAsync(userEntity, userViewModel.Password)?.Result;
            if (resultAdd?.Succeeded ?? false)
            {
                result = _mapper.Map<UserViewModel>(userEntity);
                await AddRolesToUser(userViewModel, userEntity);
            }
            return result;
        }

        public async Task<bool> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            var result = false;
            var currentUser = await GetUser();
            if (currentUser != null)
            {
                result = _userManager.ChangePasswordAsync(currentUser, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword)?.Result == IdentityResult.Success;
            }
            return result;
        }

        public async Task<UserViewModel> GetUserWithRole()
        {
            var userEntities = await GetUser();
            var result = _mapper.Map<UserViewModel>(userEntities);
            result.Roles = await _userManager.GetRolesAsync(userEntities);
            return result;
        }

        public async Task<bool> IsAdmin()
        {
            var result = false;
            var user = await GetUser();
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.IsNullOrEmpty() && roles.Contains("Administrador"))
                {
                    result = true;
                }
            }
            return result;
        }

    }
}
