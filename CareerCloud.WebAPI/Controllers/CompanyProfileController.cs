using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private readonly CompanyProfileLogic logic;

        public CompanyProfileController()
        {
            var repository = new EFGenericRepository<CompanyProfilePoco>();
            logic = new CompanyProfileLogic(repository);
        }

        [HttpGet]
        [Route("getCompanyProfile/{companyProfileId:guid}")]
        [ProducesResponseType(typeof(CompanyProfilePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyProfile(Guid companyProfileId)
        {
            var companyProfile = logic.Get(companyProfileId);
            if (companyProfile == null)
            {
                return NotFound();
            }

            return Ok(companyProfile);
        }

        [HttpGet]
        [Route("getAllCompanyProfiles")]
        [ProducesResponseType(typeof(IEnumerable<CompanyProfilePoco>), 200)]
        public ActionResult GetAllCompanyProfiles()
        {
            var companyProfiles = logic.GetAll();
            return Ok(companyProfiles);
        }

        [HttpPost]
        [Route("addCompanyProfile/{companyProfileId}")]
        [ProducesResponseType(typeof(CompanyProfilePoco), 201)]
        public ActionResult PostCompanyProfile(CompanyProfilePoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateCompanyProfile/{companyProfileId:guid}")]
        [ProducesResponseType(typeof(CompanyProfilePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyProfile(CompanyProfilePoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteCompanyProfile/{companyProfileId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
        {
            foreach (var poco in pocos)
            {
                var existingCompanyProfile = logic.Get(poco.Id);
                if (existingCompanyProfile != null)
                {
                    logic.Delete(new CompanyProfilePoco[] { existingCompanyProfile });
                }
            }
            return Ok();
        }
    }
}
