using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic logic;

        public SecurityLoginController()
        {
            var repository = new EFGenericRepository<SecurityLoginPoco>();
            logic = new SecurityLoginLogic(repository);
        }

        [HttpGet]
        [Route("getLogin/{securityLoginId}")]
        [ProducesResponseType(typeof(SecurityLoginPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLogin(Guid securityLoginId)
        {
            var securityLogin = logic.Get(securityLoginId);
            if (securityLogin == null)
            {
                return NotFound();
            }

            return Ok(securityLogin);
        }

        [HttpGet]
        [Route("getAllLogins")]
        [ProducesResponseType(typeof(IEnumerable<SecurityLoginPoco>), 200)]
        public ActionResult GetAllSecurityLogins()
        {
            var pocos = logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("addLogin/{securityLoginId}")]
        [ProducesResponseType(typeof(SecurityLoginPoco), 201)]
        public ActionResult PostSecurityLogin(SecurityLoginPoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateLogin/{securityLoginId}")]
        [ProducesResponseType(typeof(SecurityLoginPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutSecurityLogin(SecurityLoginPoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteLogin/{securityLoginId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
        {
            foreach (var poco in pocos)
            {
                var existingSecurityLogin = logic.Get(poco.Id);
                if (existingSecurityLogin != null)
                {
                    logic.Delete(new SecurityLoginPoco[] { existingSecurityLogin });
                }
            }
            return Ok();
        }
    }
}
