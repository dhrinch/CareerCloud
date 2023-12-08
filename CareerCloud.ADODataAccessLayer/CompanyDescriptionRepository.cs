using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        private readonly string connectionString = Config.GetConnectionString();
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Insert into Company_Descriptions
                                                   (Id,
                                                    Company,
                                                    LanguageId,
                                                    Company_Name,
                                                    Company_Description)
                                            values
                                                   (@Id,
                                                    @Company,
                                                    @LanguageId,
                                                    @Company_Name,
                                                    @Company_Description)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnx;
                SqlDataReader reader = (new SqlCommand("Select * from Company_Descriptions", cnx)).ExecuteReader();

                var allDescriptions = new List<CompanyDescriptionPoco>();
                while (reader.Read())
                
                {
                    allDescriptions.Add(new CompanyDescriptionPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Company = (Guid)reader["Company"],
                        LanguageId = (string)reader["LanguageId"],
                        CompanyName = (string)reader["Company_Name"],
                        CompanyDescription = (string)reader["Company_Description"]                        
                    });
                }

                cnx.Close();
                return allDescriptions;
            }
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Delete from Company_Descriptions
                                               where Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Update Company_Descriptions
                                               set Company = @Company,
                                                   LanguageID = @LanguageID,
                                                   Company_Name = @Company_Name,
                                                   Company_Description = @Company_Description
                                               where ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);                 

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }
    }
}
