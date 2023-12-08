using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        private readonly string connectionString = Config.GetConnectionString();
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Insert into Applicant_Profiles
                                                   (Id,
                                                    Login,
                                                    Current_Salary,
                                                    Current_Rate,
                                                    Currency,
                                                    Country_Code,
                                                    State_Province_Code,
                                                    Street_Address,
                                                    City_Town,
                                                    Zip_Postal_Code)
                                            values
                                                   (@Id,
                                                    @Login,
                                                    @Current_Salary,
                                                    @Current_Rate,
                                                    @Currency,
                                                    @Country_Code,
                                                    @State_Province_Code,
                                                    @Street_Address,
                                                    @City_Town,
                                                    @Zip_Postal_Code)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnx;
                SqlDataReader reader = (new SqlCommand("Select * from Applicant_Profiles", cnx)).ExecuteReader();

                var allProfiles = new List<ApplicantProfilePoco>();
                while (reader.Read())
                
                {
                    allProfiles.Add(new ApplicantProfilePoco()
                    {
                        Id = (Guid)reader["Id"],
                        Login = (Guid)reader["Login"],                        
                        CurrentSalary = reader.IsDBNull(reader.GetOrdinal("Current_Salary")) ? (decimal?)null : (decimal)reader["Current_Salary"],                        
                        CurrentRate = reader.IsDBNull(reader.GetOrdinal("Current_Rate")) ? (decimal?)null : (decimal)reader["Current_Rate"],                        
                        Currency = reader.IsDBNull(reader.GetOrdinal("Currency")) ? (string?)null : (string)reader["Currency"],                        
                        Country = reader.IsDBNull(reader.GetOrdinal("Country_Code")) ? (string?)null : (string)reader["Country_Code"],                        
                        Province = reader.IsDBNull(reader.GetOrdinal("State_Province_Code")) ? (string?)null : (string)reader["State_Province_Code"],                        
                        Street = reader.IsDBNull(reader.GetOrdinal("Street_Address")) ? (string?)null : (string)reader["Street_Address"],                        
                        City = reader.IsDBNull(reader.GetOrdinal("City_Town")) ? (string?)null : (string)reader["City_Town"],                        
                        PostalCode = reader.IsDBNull(reader.GetOrdinal("Zip_Postal_Code")) ? (string?)null : (string)reader["Zip_Postal_Code"]
                    });
                }
                cnx.Close();
                return allProfiles;
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach(var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Delete from Applicant_Profiles
                                               where Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Update Applicant_Profiles
                                               set Login = @Login,
                                                   Current_Salary = @Current_Salary,                                                    
                                                   Current_Rate = @Current_Rate,
                                                   Currency = @Currency,
                                                   Country_Code = @Country_Code,
                                                   State_Province_Code  = @State_Province_Code,
                                                   Street_Address = @Street_Address,
                                                   City_Town = @City_Town,
                                                   Zip_Postal_Code = @Zip_Postal_Code
                                               where ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.Country);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }
    }
}
