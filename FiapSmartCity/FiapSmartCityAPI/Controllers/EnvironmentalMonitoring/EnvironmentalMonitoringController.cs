using FiapSmartCityAPI.Attributes;
using FiapSmartCityServices.EnvironmentalMonitoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FiapSmartCityAPI.Controllers.EnvironmentalMonitoring
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ExtendedAuthorize(new string[] { "Administrador", "TrafficManagementController" })]
    public class EnvironmentalMonitoringController : ControllerBase
    {
        private readonly IEnvironmentalMonitoringService _environmentalMonitoringService;
        public EnvironmentalMonitoringController(IEnvironmentalMonitoringService environmentalMonitoringService)
        {
            _environmentalMonitoringService = environmentalMonitoringService;
        }


        // GET: api/<EnvironmentalMonitoringController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EnvironmentalMonitoringController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EnvironmentalMonitoringController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EnvironmentalMonitoringController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EnvironmentalMonitoringController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
