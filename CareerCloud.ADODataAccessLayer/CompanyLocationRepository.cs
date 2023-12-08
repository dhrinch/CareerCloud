using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        private readonly string connectionString = Config.GetConnectionString();
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Insert into Company_Locations
                                                   (Id,
                                                    Company,
                                                    Country_Code,
                                                    State_Province_Code,
                                                    Street_Address,
                                                    City_Town,
                                                    Zip_Postal_Code)
                                            values
                                                   (@Id,
                                                    @Company,
                                                    @Country_Code,
                                                    @State_Province_Code,
                                                    @Street_Address,
                                                    @City_Town,
                                                    @Zip_Postal_Code)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnx;
                SqlDataReader reader = (new SqlCommand("Select * from Company_Locations", cnx)).ExecuteReader();

                var allLocations = new List<CompanyLocationPoco>();
                while (reader.Read())
                {
                    allLocations.Add(new CompanyLocationPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Company = (Guid)reader["Company"],
                        CountryCode = (string)reader["Country_Code"],
                        Province = reader.IsDBNull(reader.GetOrdinal("State_Province_Code")) ? (string?)null : (string)reader["State_Province_Code"],
                        Street = reader.IsDBNull(reader.GetOrdinal("Street_Address")) ? (string?)null : (string)reader["Street_Address"],
                        City = reader.IsDBNull(reader.GetOrdinal("City_Town")) ? (string?)null : (string)reader["City_Town"],
                        PostalCode = reader.IsDBNull(reader.GetOrdinal("Zip_Postal_Code")) ? (string?)null : (string)reader["Zip_Postal_Code"]
                    });
                }

                cnx.Close();
                return allLocations;
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Delete from Company_Locations
                                               where Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Update Company_Locations
                                                 set Company = @Company,
                                                     Country_Code = @Country_Code,
                                                     State_Province_Code = @State_Province_Code,
                                                     Street_Address = @Street_Address,
                                                     City_Town = @City_Town,
                                                     Zip_Postal_Code  = @Zip_Postal_Code
                                               where ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
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
