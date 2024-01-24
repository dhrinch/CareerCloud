using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemCountryCodeController : ControllerBase
    {
        private readonly SystemCountryCodeLogic logic;

        public SystemCountryCodeController()
        {
            var repository = new EFGenericRepository<SystemCountryCodePoco>();
            logic = new SystemCountryCodeLogic(repository);
        }

        [HttpGet]
        [Route("getCountryCode/{countryCodeId}")]
        [ProducesResponseType(typeof(SystemCountryCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSystemCountryCode(string systemCountryCodeId)
        {
            var countryCode = logic.Get(systemCountryCodeId);
            if (countryCode == null)
            {
                return NotFound();
            }

            return Ok(countryCode);
        }

        [HttpGet]
        [Route("getAllCountryCodes")]
        [ProducesResponseType(typeof(IEnumerable<SystemCountryCodePoco>), 200)]
        public ActionResult GetAllCountryCodes()
        {
            var pocos = logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("addCountryCode/{countryCodeId}")]
        [ProducesResponseType(typeof(SystemCountryCodePoco), 201)]
        public ActionResult PostSystemCountryCode(SystemCountryCodePoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateCountryCode/{countryCodeId}")]
        [ProducesResponseType(typeof(SystemCountryCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutSystemCountryCode(SystemCountryCodePoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteCountryCode/{countryCodeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
        {
            foreach (var poco in pocos)
            {
                var existingCountryCode = logic.Get(poco.Code);
                if (existingCountryCode != null)
                {
                    logic.Delete(existingCountryCode);
                }
            }
            return Ok();
        }
    }
}
