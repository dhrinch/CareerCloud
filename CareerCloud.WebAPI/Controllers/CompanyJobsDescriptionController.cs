using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobsDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic logic;

        public CompanyJobsDescriptionController()
        {
            var repository = new EFGenericRepository<CompanyJobDescriptionPoco>();
            logic = new CompanyJobDescriptionLogic(repository);
        }

        [HttpGet]
        [Route("getJobDescription/{companyJobDescriptionId}")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobsDescription(Guid companyJobDescriptionId)
        {
            var companyJobDescription = logic.Get(companyJobDescriptionId);
            if (companyJobDescription == null)
            {
                return NotFound();
            }

            return Ok(companyJobDescription);
        }

        [HttpGet]
        [Route("getAllJobDescriptions")]
        [ProducesResponseType(typeof(IEnumerable<CompanyJobDescriptionPoco>), 200)]
        public ActionResult GetAllCompanyJobDescriptions()
        {
            var companyDescriptions = logic.GetAll();
            return Ok(companyDescriptions);
        }

        [HttpPost]
        [Route("addJobDescription/{companyJobDescriptionId}")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco), 201)]
        public ActionResult PostCompanyJobsDescription(CompanyJobDescriptionPoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateJobDescription/{companyJobDescriptionId}")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyJobsDescription(CompanyJobDescriptionPoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteJobDescription/{companyJobDescriptionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
        {            
            foreach (var poco in pocos)
            {
                var existingJobDescription = logic.Get(poco.Id);
                if (existingJobDescription != null)
                {
                    logic.Delete(new CompanyJobDescriptionPoco[] { existingJobDescription });
                }
            }
            return Ok();
        }
    }
}
