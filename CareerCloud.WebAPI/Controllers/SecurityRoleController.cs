using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityRoleController : ControllerBase
    {
        private readonly SecurityRoleLogic logic;

        public SecurityRoleController()
        {
            var repository = new EFGenericRepository<SecurityRolePoco>();
            logic = new SecurityRoleLogic(repository);
        }

        [HttpGet]
        [Route("getSecurityRole/{securityRoleId}")]
        [ProducesResponseType(typeof(SecurityRolePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityRole(Guid securityRoleId)
        {
            var securityRole = logic.Get(securityRoleId);
            if (securityRole == null)
            {
                return NotFound();
            }

            return Ok(securityRole);
        }

        [HttpGet]
        [Route("getAllSecurityRoles")]
        [ProducesResponseType(typeof(IEnumerable<SecurityRolePoco>), 200)]
        public ActionResult GetAllSecurityRoles()
        {
            var pocos = logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("addSecurityRole/{securityRoleId}")]
        [ProducesResponseType(typeof(SecurityRolePoco), 201)]
        public ActionResult PostSecurityRole(SecurityRolePoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateSecurityRole/{securityRoleId}")]
        [ProducesResponseType(typeof(SecurityRolePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutSecurityRole(SecurityRolePoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteSecurityRole/{securityRoleId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] pocos)
        {
            foreach (var poco in pocos)
            {
                var existingSecurityRole = logic.Get(poco.Id);
                if (existingSecurityRole != null)
                {
                    logic.Delete(new SecurityRolePoco[] { existingSecurityRole });
                }
            }
            return Ok();
        }
    }
}
