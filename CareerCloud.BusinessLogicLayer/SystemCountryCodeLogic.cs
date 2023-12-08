using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic
    {
        private IDataRepository<SystemCountryCodePoco> dataRepository;

        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> dataRepository)
        {
            this.dataRepository = dataRepository;
        }
        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            this.dataRepository.Add(pocos);
        }

        public virtual SystemCountryCodePoco Get(string id)
        {
            return dataRepository.GetSingle(c => c.Code == id);
        }

        public virtual List<SystemCountryCodePoco> GetAll()
        {
            return dataRepository.GetAll().ToList();
        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            this.dataRepository.Update(pocos);
        }

        public void Delete(SystemCountryCodePoco pocos)
        {
            this.dataRepository.Remove(pocos);
        }

        protected void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                    exceptions.Add(new ValidationException(900, $"Code in {poco} cannot be empty"));

                if (string.IsNullOrEmpty(poco.Name))
                    exceptions.Add(new ValidationException(901, $"Name in {poco} cannot be empty"));
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
