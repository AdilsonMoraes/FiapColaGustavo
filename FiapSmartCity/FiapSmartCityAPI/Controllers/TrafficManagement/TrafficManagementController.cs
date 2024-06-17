using FiapSmartCityAPI.Attributes;
using FiapSmartCityServices.PublicSecurity;
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


        // GET: api/<TrafficManagementController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TrafficManagementController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TrafficManagementController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TrafficManagementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TrafficManagementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
