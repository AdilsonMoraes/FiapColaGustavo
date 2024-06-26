using FiapSmartCityAPI.Attributes;
using FiapSmartCityDomain.Authentication.ViewModel;
using FiapSmartCityDomain.TrafficManagement.ViewModel;
using FiapSmartCityServices.TrafficManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FiapSmartCityAPI.Controllers.TrafficManagement
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ExtendedAuthorize(new string[] { "Administrador", "TrafficManagement" })]
    public class TrafficManagementController : ControllerBase
    {
        private readonly ITrafficManagementService _trafficManagementService;
        public TrafficManagementController (ITrafficManagementService trafficManagementService)
        {
            _trafficManagementService = trafficManagementService;
        }

        [ExtendedAuthorize(new string[] { "Administrador", "TrafficManagement" })]
        [HttpGet]
        public async Task<ICollection<AccidentsViewModel>> GetAccidents()
             => await _trafficManagementService.GetAccidents();

        [ExtendedAuthorize(new string[] { "Administrador" })]
        [HttpPost]
        public async Task<AccidentsViewModel> CreateAccidents([FromBody] AccidentsViewModel userViewModel)
    => await _trafficManagementService.CreateAccidents(userViewModel);
    }
}
