using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyDescriptionController : ControllerBase
    {
        private readonly CompanyDescriptionLogic logic;

        public CompanyDescriptionController()
        {
            var repository = new EFGenericRepository<CompanyDescriptionPoco>();
            logic = new CompanyDescriptionLogic(repository);
        }

        [HttpGet]
        [Route("getCompanyDescription/{companyDescriptionId}")]
        [ProducesResponseType(typeof(CompanyDescriptionPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyDescription(Guid companyDescriptionId)
        {
            var companyDescription = logic.Get(companyDescriptionId);
            if (companyDescription == null)
            {
                return NotFound();
            }

            return Ok(companyDescription);
        }

        [HttpGet]
        [Route("getAllCompanyDescriptions")]
        [ProducesResponseType(typeof(IEnumerable<CompanyDescriptionPoco>), 200)]
        public ActionResult GetAllCompanyDescriptions()
        {
            var companyDescriptions = logic.GetAll();
            return Ok(companyDescriptions);
        }

        [HttpPost]
        [Route("addCompanyDescription/{companyDescriptionId}")]
        [ProducesResponseType(typeof(CompanyDescriptionPoco), 201)]
        public ActionResult PostCompanyDescription(CompanyDescriptionPoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateCompanyDescription/{companyDescriptionId}")]
        [ProducesResponseType(typeof(CompanyDescriptionPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyDescription(CompanyDescriptionPoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteCompanyDescription/{companyDescriptionId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteCompanyDescription([FromBody] CompanyDescriptionPoco[]pocos)
        {            
            foreach (var poco in pocos)
            {
                var existingCompanyDescription = logic.Get(poco.Id);
                if (existingCompanyDescription != null)
                {
                    logic.Delete(new CompanyDescriptionPoco[] { existingCompanyDescription });
                }
            }
            return Ok();
        }
    }
}
