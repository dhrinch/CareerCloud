using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        private readonly string connectionString = Config.GetConnectionString();
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Insert into Company_Jobs_Descriptions
                                                   (Id,
                                                    Job,
                                                    Job_Name,
                                                    Job_Descriptions)
                                            values
                                                   (@Id,
                                                    @Job,
                                                    @Job_Name,
                                                    @Job_Descriptions)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);

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

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnx;
                SqlDataReader reader = (new SqlCommand("Select * from Company_Jobs_Descriptions", cnx)).ExecuteReader();

                var allDescriptions = new List<CompanyJobDescriptionPoco>();
                while (reader.Read())
                {
                    allDescriptions.Add(new CompanyJobDescriptionPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Job = (Guid)reader["Job"],
                        JobName = reader.IsDBNull(reader.GetOrdinal("Job_Name")) ? (string?)null : (string)reader["Job_Name"],
                        JobDescriptions = reader.IsDBNull(reader.GetOrdinal("Job_Descriptions")) ? (string?)null : (string)reader["Job_Descriptions"]
                    });
                }
                cnx.Close();
                return allDescriptions;
            }
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Delete from Company_Jobs_Descriptions
                                               where Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Update Company_Jobs_Descriptions
                                               set Job = @Job,
                                                   Job_Name = @Job_Name,
                                                   Job_Descriptions = @Job_Descriptions
                                               where ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }
    }
}
