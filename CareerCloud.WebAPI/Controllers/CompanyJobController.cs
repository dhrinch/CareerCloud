using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobController : ControllerBase
    {
        private readonly CompanyJobLogic logic;

        public CompanyJobController()
        {
            var repository = new EFGenericRepository<CompanyJobPoco>();
            logic = new CompanyJobLogic(repository);
        }

        [HttpGet]
        [Route("getJob/{companyJobId}")]
        [ProducesResponseType(typeof(CompanyJobPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJob(Guid companyJobId)
        {
            var companyJob = logic.Get(companyJobId);
            if (companyJob == null)
            {
                return NotFound();
            }

            return Ok(companyJob);
        }

        [HttpGet]
        [Route("getAllJobs")]
        [ProducesResponseType(typeof(IEnumerable<CompanyJobPoco>), 200)]
        public ActionResult GetAllCompanyJobs()
        {
            var companyJobs = logic.GetAll();
            return Ok(companyJobs);
        }

        [HttpPost]
        [Route("addJob/{companyJobId}")]
        [ProducesResponseType(typeof(CompanyJobPoco), 201)]
        public ActionResult PostCompanyJob(CompanyJobPoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateJob/{companyJobId}")]
        [ProducesResponseType(typeof(CompanyJobPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyJob(CompanyJobPoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteJob/{companyJobId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteCompanyJob([FromBody]CompanyJobPoco[] pocos)
        {            
            foreach (var poco in pocos)
            {
                var existingJob = logic.Get(poco.Id);
                if (existingJob != null)
                {
                    logic.Delete(new CompanyJobPoco[] { existingJob });
                }
            }
            return Ok();
        }
    }
}
