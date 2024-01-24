using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantEducationController : ControllerBase
    {
        private readonly ApplicantEducationLogic logic;

        public ApplicantEducationController()
        {
            var repository = new EFGenericRepository<ApplicantEducationPoco>();
            logic = new ApplicantEducationLogic(repository);
        }

        [HttpGet]
        [Route("getEducation/{applicantEducationId:guid}")]
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantEducation(Guid applicantEducationId)
        {
            var education = logic.Get(applicantEducationId);
            if (education == null)
            {
                return NotFound();
            }
            return Ok(education);
        }

        [HttpGet]
        [Route("getAllEducations")]
        [ProducesResponseType(typeof(IEnumerable<ApplicantEducationPoco>), 200)]
        [ProducesResponseType(500)]
        public ActionResult GetAllApplicantEducation()
        {
            try
            {
                var pocos = logic.GetAll();
                return Ok(pocos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("addEducation")]
        [ProducesResponseType(typeof(ApplicantEducationPoco), 201)]
        [ProducesResponseType(400)]
        public ActionResult PostApplicantEducation(ApplicantEducationPoco[] poco)
        {
            if (poco == null || poco.Length == 0)
            {
                return BadRequest("Data is empty.");
            }

            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateEducation/{applicantEducationId:guid}")]
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutApplicantEducation(ApplicantEducationPoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteEducation/{applicantEducationId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {
            foreach (var poco in pocos)
            {
                var existingEducation = logic.Get(poco.Id);
                if (existingEducation != null)
                {
                    logic.Delete(new ApplicantEducationPoco[] { existingEducation });
                }
            }

            return Ok();
        }
    }
}
