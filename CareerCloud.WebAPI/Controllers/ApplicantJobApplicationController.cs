using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly ApplicantJobApplicationLogic logic;

        public ApplicantJobApplicationController()
        {
            var repository = new EFGenericRepository<ApplicantJobApplicationPoco>();
            logic = new ApplicantJobApplicationLogic(repository);
        }

        [HttpGet]
        [Route("getApplication/{applicantJobApplicationId}")]
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco),200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantJobApplication(Guid applicantJobApplicationId)
        {
            var application = logic.Get(applicantJobApplicationId);
            if (application == null)
            {
                return NotFound();
            }

            return Ok(application);
        }

        [HttpGet]
        [Route("getAllApplications")]
        [ProducesResponseType(typeof(IEnumerable<ApplicantJobApplicationPoco>), 200)]
        public ActionResult GetAllApplicantJobApplications()
        {
            var applications = logic.GetAll();
            return Ok(applications);
        }

        [HttpPost]
        [Route("addApplication/{applicantJobApplicationId}")]
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco), 201)]
        public ActionResult PostApplicantJobApplication(ApplicantJobApplicationPoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateApplication/{applicantJobApplicationId}")]
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutApplicantJobApplication(ApplicantJobApplicationPoco[] poco)
        {            
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteApplication/{applicantJobApplicationId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)
        {
            foreach (var poco in pocos)
            {
                var existingApplication = logic.Get(poco.Id);
                if (existingApplication != null)
                {
                    logic.Delete(new ApplicantJobApplicationPoco[] { existingApplication });
                }
            }
            return Ok();
        }
    }
}
