using FiapSmartCityAPI.Attributes;
using FiapSmartCityServices.TrafficManagement;
using FiapSmartCityServices.WasteManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FiapSmartCityAPI.Controllers.WasteManagement
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ExtendedAuthorize(new string[] { "Administrador", "TrafficManagement" })]
    public class WasteManagementController : ControllerBase
    {
        private readonly IWasteManagementService _wasteManagementService;
        public WasteManagementController(IWasteManagementService wasteManagementService)
        {
            _wasteManagementService = wasteManagementService;
        }

        // GET: api/<WasteManagementController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WasteManagementController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<WasteManagementController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<WasteManagementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<WasteManagementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
