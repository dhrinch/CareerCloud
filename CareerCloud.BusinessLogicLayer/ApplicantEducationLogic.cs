using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {
        }

        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Major))
                    exceptions.Add(new ValidationException(107, $"Major in {poco} cannot be empty"));
                else if (poco.Major.Length < 3)
                    exceptions.Add(new ValidationException(107, $"Major in {poco} ({poco.Major}) cannot be less than 3 characters"));

                if (poco.StartDate > DateTime.Today)
                    exceptions.Add(new ValidationException(108, $"StartDate in {poco} ({poco.StartDate}) cannot be greater than today ({DateTime.Today})"));

                if (poco.CompletionDate < poco.StartDate)
                    exceptions.Add(new ValidationException(109, $"CompletionDate in {poco} ({poco.CompletionDate}) cannot be earlier than StartDate ({poco.StartDate})"));
            }
            
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
