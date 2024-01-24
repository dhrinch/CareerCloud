using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsRoleController : ControllerBase
    {
        private readonly SecurityLoginsRoleLogic logic;

        public SecurityLoginsRoleController()
        {
            var repository = new EFGenericRepository<SecurityLoginsRolePoco>();
            logic = new SecurityLoginsRoleLogic(repository);
        }

        [HttpGet]
        [Route("getLoginRole/{securityLoginsRoleId}")]
        [ProducesResponseType(typeof(SecurityLoginsRolePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLoginsRole(Guid securityLoginsRoleId)
        {
            var securityLoginsRole = logic.Get(securityLoginsRoleId);
            if (securityLoginsRole == null)
            {
                return NotFound();
            }

            return Ok(securityLoginsRole);
        }

        [HttpGet]
        [Route("getAllLoginRoles")]
        [ProducesResponseType(typeof(IEnumerable<SecurityLoginsRolePoco>), 200)]
        public ActionResult GetAllSecurityLoginRoles()
        {
            var pocos = logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("addLoginRole/{securityLoginsRoleId}")]
        [ProducesResponseType(typeof(SecurityLoginsRolePoco), 201)]
        public ActionResult PostSecurityLoginRole(SecurityLoginsRolePoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateLoginRole/{securityLoginsRoleId}")]
        [ProducesResponseType(typeof(SecurityLoginsRolePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutSecurityLoginRole(SecurityLoginsRolePoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteLoginRole/{securityLoginsRoleId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] pocos )
        {
            foreach (var poco in pocos)
            {
                var existingRole = logic.Get(poco.Id);
                if (existingRole != null)
                {
                    logic.Delete(new SecurityLoginsRolePoco[] { existingRole });
                }
            }

            return Ok();
        }
    }
}
