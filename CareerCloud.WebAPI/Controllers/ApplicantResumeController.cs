using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantResumeController : ControllerBase
    {
        private readonly ApplicantResumeLogic logic;

        public ApplicantResumeController()
        {
            var repository = new EFGenericRepository<ApplicantResumePoco>();
            logic = new ApplicantResumeLogic(repository);
        }

        [HttpGet]
        [Route("getResume/{applicantResumeId}")]
        [ProducesResponseType(typeof(ApplicantResumePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantResume(Guid applicantResumeId)
        {
            var resume = logic.Get(applicantResumeId);
            if (resume == null)
            {
                return NotFound();
            }

            return Ok(resume);
        }

        [HttpGet]
        [Route("getAllResumes")]
        [ProducesResponseType(typeof(IEnumerable<ApplicantResumePoco>), 200)]
        public ActionResult GetAllApplicantResumes()
        {
            var resumes = logic.GetAll();
            return Ok(resumes);
        }

        [HttpPost]
        [Route("addResume/{applicantResumeId}")]
        [ProducesResponseType(typeof(ApplicantResumePoco), 201)]
        public ActionResult PostApplicantResume(ApplicantResumePoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateResume/{applicantResumeId}")]
        [ProducesResponseType(typeof(ApplicantResumePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutApplicantResume(ApplicantResumePoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteResume/{applicantResumeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteApplicantResume([FromBody]ApplicantResumePoco[] pocos)
        {   
            foreach (var poco in pocos)
            {
                var existingResume = logic.Get(poco.Id);
                if (existingResume != null)
                {
                    logic.Delete(new ApplicantResumePoco[] { existingResume });
                }
            }
            return Ok();
        }
    }
}
