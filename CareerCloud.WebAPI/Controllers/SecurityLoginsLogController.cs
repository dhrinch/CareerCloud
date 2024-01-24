using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly SecurityLoginsLogLogic logic;

        public SecurityLoginsLogController()
        {
            var repository = new EFGenericRepository<SecurityLoginsLogPoco>();
            logic = new SecurityLoginsLogLogic(repository);
        }

        [HttpGet]
        [Route("getLog/{securityLoginsLogId}")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLoginLog(Guid securityLoginsLogId)
        {
            var securityLoginsLog = logic.Get(securityLoginsLogId);
            if (securityLoginsLog == null)
            {
                return NotFound();
            }

            return Ok(securityLoginsLog);
        }

        [HttpGet]
        [Route("getAllLogs")]
        [ProducesResponseType(typeof(IEnumerable<SecurityLoginsLogPoco>), 200)]
        public ActionResult GetAllSecurityLoginsLog()
        {
            var pocos = logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("addLog/{securityLoginsLogId}")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco), 201)]
        public ActionResult PostSecurityLoginLog(SecurityLoginsLogPoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateLog/{securityLoginsLogId}")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutSecurityLoginLog(SecurityLoginsLogPoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteLog/{securityLoginsLogId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] pocos)
        {
            foreach (var poco in pocos)
            {
                var existingLoginLog = logic.Get(poco.Id);
                if (existingLoginLog != null)
                {
                    logic.Delete(new SecurityLoginsLogPoco[] { existingLoginLog });
                }
            }
            return Ok();
        }
    }
}
