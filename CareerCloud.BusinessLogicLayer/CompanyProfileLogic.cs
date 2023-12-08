using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Text.RegularExpressions;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();            
            Regex validateURLRegex = new Regex("\b(?:https?://)?(?:www\\.)?\\w+\\.(?:com|biz|ca)\b");
            Regex validatePhoneRegex = new Regex("^\\d{3}-\\d{3}-\\d{4}$");
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyWebsite) || !validateURLRegex.IsMatch(poco.CompanyWebsite))
                    exceptions.Add(new ValidationException(600, $"CompanyWebsite for CompanyProfile {poco.CompanyWebsite} must end with one of the following extensions – \".ca\", \".com\", \".biz\""));

                if (string.IsNullOrEmpty(poco.ContactPhone) || !validatePhoneRegex.IsMatch(poco.ContactPhone))
                    exceptions.Add(new ValidationException(601, $"ContactPhone for CompanyProfile {poco.ContactPhone} must correspond to a valid phone number"));
            }            

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
