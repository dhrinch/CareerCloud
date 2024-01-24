using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly CompanyJobEducationLogic logic;

        public CompanyJobEducationController()
        {
            var repository = new EFGenericRepository<CompanyJobEducationPoco>();
            logic = new CompanyJobEducationLogic(repository);
        }

        [HttpGet]
        [Route("getJobEducation/{companyJobEducationId}")]
        [ProducesResponseType(typeof(CompanyJobEducationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobEducation(Guid companyJobEducationId)
        {
            var companyJobEducation = logic.Get(companyJobEducationId);
            if (companyJobEducation == null)
            {
                return NotFound();
            }

            return Ok(companyJobEducation);
        }

        [HttpGet]
        [Route("getAllJobEducations")]
        [ProducesResponseType(typeof(IEnumerable<CompanyJobEducationPoco>), 200)]
        public ActionResult GetAllCompanyJobEducations()
        {
            var companyEducations = logic.GetAll();
            return Ok(companyEducations);
        }

        [HttpPost]
        [Route("addJobEducation/{companyJobEducationId}")]
        [ProducesResponseType(typeof(CompanyJobEducationPoco), 201)]
        public ActionResult PostCompanyJobEducation(CompanyJobEducationPoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateJobEducation/{companyJobEducationId}")]
        [ProducesResponseType(typeof(CompanyJobEducationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyJobEducation(CompanyJobEducationPoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteJobEducation/{companyJobEducationId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteCompanyJobEducation([FromBody] CompanyJobEducationPoco[]pocos)
        {            
            foreach (var poco in pocos)
            {
                var existingJobEducation = logic.Get(poco.Id);
                if (existingJobEducation != null)
                {
                    logic.Delete(new CompanyJobEducationPoco[] { existingJobEducation });
                }
            }
            return Ok();
        }
    }
}
