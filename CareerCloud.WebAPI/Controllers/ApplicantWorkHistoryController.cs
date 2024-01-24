using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantWorkHistoryController : ControllerBase
    {
        private readonly ApplicantWorkHistoryLogic logic;

        public ApplicantWorkHistoryController()
        {
            var repository = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            logic = new ApplicantWorkHistoryLogic(repository);
        }

        [HttpGet]
        [Route("getHistory/{applicantWorkHistoryId}")]
        [ProducesResponseType(typeof(ApplicantWorkHistoryPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantWorkHistory(Guid applicantWorkHistoryId)
        {
            var workHistory = logic.Get(applicantWorkHistoryId);
            if (workHistory == null)
            {
                return NotFound();
            }

            return Ok(workHistory);
        }

        [HttpGet]
        [Route("getAllHistory")]
        [ProducesResponseType(typeof(IEnumerable<ApplicantWorkHistoryPoco>), 200)]
        public ActionResult GetAllApplicantWorkHistory()
        {
            var workHistory = logic.GetAll();
            return Ok(workHistory);
        }

        [HttpPost]
        [Route("addHistory/{applicantWorkHistoryId}")]
        [ProducesResponseType(typeof(ApplicantWorkHistoryPoco), 201)]
        public ActionResult PostApplicantWorkHistory(ApplicantWorkHistoryPoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateHistory/{applicantWorkHistoryId}")]
        [ProducesResponseType(typeof(ApplicantWorkHistoryPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutApplicantWorkHistory(ApplicantWorkHistoryPoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteHistory/{applicantWorkHistoryId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
        {            
            foreach (var poco in pocos)
            {
                var existingWorkHistory = logic.Get(poco.Id);
                if (existingWorkHistory != null)
                {
                    logic.Delete(new ApplicantWorkHistoryPoco[] { existingWorkHistory });
                }
            }
            return Ok();
        }
    }
}
