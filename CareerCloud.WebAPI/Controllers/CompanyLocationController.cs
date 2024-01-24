using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyLocationController : ControllerBase
    {
        private readonly CompanyLocationLogic logic;

        public CompanyLocationController()
        {
            var repository = new EFGenericRepository<CompanyLocationPoco>();
            logic = new CompanyLocationLogic(repository);
        }

        [HttpGet]
        [Route("getLocation/{companyLocationId}")]
        [ProducesResponseType(typeof(CompanyLocationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyLocation(Guid companyLocationId)
        {
            var companyLocation = logic.Get(companyLocationId);
            if (companyLocation == null)
            {
                return NotFound();
            }

            return Ok(companyLocation);
        }

        [HttpGet]
        [Route("getAllLocations")]
        [ProducesResponseType(typeof(IEnumerable<CompanyLocationPoco>), 200)]
        public ActionResult GetAllCompanyLocations()
        {
            var companyLocations = logic.GetAll();
            return Ok(companyLocations);
        }

        [HttpPost]
        [Route("addLocation/{companyLocationId}")]
        [ProducesResponseType(typeof(CompanyLocationPoco), 201)]
        public ActionResult PostCompanyLocation(CompanyLocationPoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateLocation/{companyLocationId}")]
        [ProducesResponseType(typeof(CompanyLocationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutCompanyLocation(CompanyLocationPoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteLocation/{companyLocationId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
        {            
            foreach (var poco in pocos)
            {
                var existingLocation = logic.Get(poco.Id);
                if (existingLocation != null)
                {
                    logic.Delete(new CompanyLocationPoco[] { existingLocation });
                }
            }
            return Ok();
        }
    }
}
