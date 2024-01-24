using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobSkillController : ControllerBase
    {
        private readonly CompanyJobSkillLogic logic;

        public CompanyJobSkillController()
        {
            var repository = new EFGenericRepository<CompanyJobSkillPoco>();
            logic = new CompanyJobSkillLogic(repository);
        }

        [HttpGet]
        [Route("getJobSkill/{companyJobSkillId}")]
        [ProducesResponseType(typeof(CompanyJobSkillPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobSkill(Guid companyJobSkillId)
        {
            var companyJobSkill = logic.Get(companyJobSkillId);
            if (companyJobSkill == null)
            {
                return NotFound();
            }

            return Ok(companyJobSkill);
        }

        [HttpGet]
        [Route("getAllJobSkills")]
        [ProducesResponseType(typeof(IEnumerable<CompanyJobSkillPoco>), 200)]
        public ActionResult GetAllCompanyJobSkills()
        {
            var companyJobSkills = logic.GetAll();
            return Ok(companyJobSkills);
        }

        [HttpPost]
        [Route("addJobSkill/{companyJobSkillId}")]
        [ProducesResponseType(typeof(CompanyJobSkillPoco), 201)]
        public ActionResult PostCompanyJobSkill(CompanyJobSkillPoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateJobSkill/{companyJobSkillId}")]
        [ProducesResponseType(typeof(CompanyJobSkillPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyJobSkill(CompanyJobSkillPoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteJobSkill/{companyJobSkillId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos)
        {    
            foreach (var poco in pocos)
            {
                var existingSkill = logic.Get(poco.Id);
                if (existingSkill != null)
                {
                    logic.Delete(new CompanyJobSkillPoco[] { existingSkill });
                }
            }
            return Ok();
        }
    }
}
