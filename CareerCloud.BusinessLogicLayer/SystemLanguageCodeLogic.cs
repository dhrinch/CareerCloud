using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic
    {
        private IDataRepository<SystemLanguageCodePoco> dataRepository;

        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> dataRepository)
        {
            this.dataRepository = dataRepository;
        }
        public void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            this.dataRepository.Add(pocos);
        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            this.dataRepository.Update(pocos);
        }

        public void Delete(SystemLanguageCodePoco pocos)
        {
            this.dataRepository.Remove(pocos);
        }

        public virtual SystemLanguageCodePoco Get(string id)
        {
            return dataRepository.GetSingle(c => c.LanguageID == id);
        }

        public virtual List<SystemLanguageCodePoco> GetAll()
        {
            return dataRepository.GetAll().ToList();
        }

        protected void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                    exceptions.Add(new ValidationException(1000, $"LanguageID in {poco} cannot be empty"));

                if (string.IsNullOrEmpty(poco.Name))
                    exceptions.Add(new ValidationException(1001, $"Name in {poco} cannot be empty"));
                
                if (string.IsNullOrEmpty(poco.NativeName))
                    exceptions.Add(new ValidationException(1002, $"NativeName in {poco} cannot be empty"));
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
