using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantSkillController : ControllerBase
    {
        private readonly ApplicantSkillLogic logic;

        public ApplicantSkillController()
        {
            var repository = new EFGenericRepository<ApplicantSkillPoco>();
            logic = new ApplicantSkillLogic(repository);
        }

        [HttpGet]
        [Route("getSkill/{applicantSkillId}")]
        [ProducesResponseType(typeof(ApplicantSkillPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantSkill(Guid applicantSkillId)
        {
            var skill = logic.Get(applicantSkillId);
            if (skill == null)
            {
                return NotFound();
            }

            return Ok(skill);
        }

        [HttpGet]
        [Route("getAllSkills")]
        [ProducesResponseType(typeof(IEnumerable<ApplicantSkillPoco>), 200)]
        public ActionResult GetAllApplicantSkills()
        {
            var skills = logic.GetAll();
            return Ok(skills);
        }

        [HttpPost]
        [Route("addSkill/{applicantSkillId}")]
        [ProducesResponseType(typeof(ApplicantSkillPoco), 201)]
        public ActionResult PostApplicantSkill(ApplicantSkillPoco[] skill)
        {
            logic.Add(skill);
            return Ok();
        }

        [HttpPut]
        [Route("updateSkill/{applicantSkillId}")]
        [ProducesResponseType(typeof(ApplicantSkillPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutApplicantSkill(ApplicantSkillPoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteSkill/{applicantSkillId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteApplicantSkill([FromBody]ApplicantSkillPoco[] pocos)
        {            
            foreach (var poco in pocos)
            {
                var existingSkill = logic.Get(poco.Id);
                if (existingSkill != null)
                {
                    logic.Delete(new ApplicantSkillPoco[] { existingSkill });
                }
            }
            return Ok();
        }
    }
}
