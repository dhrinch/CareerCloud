using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantProfileController : ControllerBase
    {
        private readonly ApplicantProfileLogic logic;

        public ApplicantProfileController()
        {
            var repository = new EFGenericRepository<ApplicantProfilePoco>();
            logic = new ApplicantProfileLogic(repository);
        }

        [HttpGet]
        [Route("getProfile/{applicantProfileId}")]
        [ProducesResponseType(typeof(ApplicantProfilePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantProfile(Guid applicantProfileId)
        {
            var profile = logic.Get(applicantProfileId);
            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

        [HttpGet]
        [Route("getAllProfiles")]
        [ProducesResponseType(typeof(IEnumerable<ApplicantProfilePoco>), 200)]
        public ActionResult GetAllApplicantProfiles()
        {
            var profiles = logic.GetAll();
            return Ok(profiles);
        }

        [HttpPost]
        [Route("addProfile/{applicantProfileId}")]
        [ProducesResponseType(typeof(ApplicantProfilePoco), 201)]
        public ActionResult PostApplicantProfile(ApplicantProfilePoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateProfile/{applicantProfileId}")]
        [ProducesResponseType(typeof(ApplicantProfilePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutApplicantProfile(ApplicantProfilePoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteProfile/{applicantProfileId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            foreach (var poco in pocos)
            {
                var existingProfile = logic.Get(poco.Id);
                if (existingProfile != null)
                {
                    logic.Delete(new ApplicantProfilePoco[] { existingProfile });
                }
            }
            return Ok();
        }
    }
}
