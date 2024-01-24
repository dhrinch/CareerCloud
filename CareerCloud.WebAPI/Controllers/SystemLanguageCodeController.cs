using Microsoft.AspNetCore.Mvc;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.Pocos;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly SystemLanguageCodeLogic logic;

        public SystemLanguageCodeController()
        {
            var repository = new EFGenericRepository<SystemLanguageCodePoco>();
            logic = new SystemLanguageCodeLogic(repository);
        }

        [HttpGet]
        [Route("getLanguageCode/{languageCodeId}")]
        [ProducesResponseType(typeof(SystemLanguageCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSystemLanguageCode(string systemLanguageCodeId)
        {
            var languageCode = logic.Get(systemLanguageCodeId);
            if (languageCode == null)
            {
                return NotFound();
            }

            return Ok(languageCode);
        }

        [HttpGet]
        [Route("getAllLanguageCodes")]
        [ProducesResponseType(typeof(IEnumerable<SystemLanguageCodePoco>), 200)]
        public ActionResult GetAllLanguageCodes()
        {
            var pocos = logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("addLanguageCode/{languageCodeId}")]
        [ProducesResponseType(typeof(SystemLanguageCodePoco), 201)]
        public ActionResult PostSystemLanguageCode(SystemLanguageCodePoco[] poco)
        {
            logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("updateLanguageCode/{languageCodeId}")]
        [ProducesResponseType(typeof(SystemLanguageCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult PutSystemLanguageCode(SystemLanguageCodePoco[] poco)
        {
            logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("deleteLanguageCode/{languageCodeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult DeleteSystemLanguageCode([FromBody]SystemLanguageCodePoco[] pocos)
        {
            foreach (var poco in pocos)
            {
                var existingLanguageCode = logic.Get(poco.LanguageID);
                if (existingLanguageCode != null)
                {
                    logic.Delete(existingLanguageCode);
                }
            }
            return Ok();
        }
    }
}
