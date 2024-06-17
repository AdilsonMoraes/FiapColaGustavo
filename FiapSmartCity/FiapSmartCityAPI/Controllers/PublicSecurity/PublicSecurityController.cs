using FiapSmartCityAPI.Attributes;
using FiapSmartCityServices.EnvironmentalMonitoring;
using FiapSmartCityServices.PublicSecurity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FiapSmartCityAPI.Controllers.PublicSecurity
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    [ExtendedAuthorize(new string[] { "Administrador", "PublicSecurity" })]
    public class PublicSecurityController : ControllerBase
    {
        private readonly IPublicSecurityService _publicSecurityService;
        public PublicSecurityController(IPublicSecurityService publicSecurityService)
        {
            _publicSecurityService = publicSecurityService;
        }


        // GET: api/<PublicSecurityController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PublicSecurityController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PublicSecurityController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PublicSecurityController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PublicSecurityController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
