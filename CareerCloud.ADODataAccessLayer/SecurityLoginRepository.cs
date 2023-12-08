using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        private readonly string connectionString = Config.GetConnectionString();
        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Insert into Security_Logins
                                                   (Id,
                                                    Login,
                                                    Password,
                                                    Created_Date,
                                                    Password_Update_Date,
                                                    Agreement_Accepted_Date,
                                                    Is_Locked,
                                                    Is_Inactive,
                                                    Email_Address,
                                                    Phone_Number,
                                                    Full_Name,
                                                    Force_Change_Password,
                                                    Prefferred_Language)
                                            values
                                                   (@Id,
                                                    @Login,
                                                    @Password,
                                                    @Created_Date,
                                                    @Password_Update_Date,
                                                    @Agreement_Accepted_Date,
                                                    @Is_Locked,
                                                    @Is_Inactive,
                                                    @Email_Address,
                                                    @Phone_Number,
                                                    @Full_Name,
                                                    @Force_Change_Password,
                                                    @Prefferred_Language)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Password", poco.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", poco.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", poco.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", poco.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", poco.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", poco.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", poco.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);

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

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnx;
                SqlDataReader reader = (new SqlCommand("Select * from Security_Logins", cnx)).ExecuteReader();

                var allLogins = new List<SecurityLoginPoco>();
                while (reader.Read())
                {
                    allLogins.Add(new SecurityLoginPoco()
                    {
                        Id = (Guid)reader["Id"],
                        Login = (string)reader["Login"],
                        Password = (string)reader["Password"],
                        Created = (DateTime)reader["Created_Date"],
                        PasswordUpdate = reader.IsDBNull(reader.GetOrdinal("Password_Update_Date")) ? (DateTime?)null : (DateTime)reader["Password_Update_Date"],
                        AgreementAccepted = reader.IsDBNull(reader.GetOrdinal("Agreement_Accepted_Date")) ? (DateTime?)null : (DateTime)reader["Agreement_Accepted_Date"],
                        IsLocked = (bool)reader["Is_Locked"],
                        IsInactive = (bool)reader["Is_Inactive"],
                        EmailAddress = (string)reader["Email_Address"],
                        PhoneNumber = reader.IsDBNull(reader.GetOrdinal("Phone_Number")) ? (string?)null : (string)reader["Phone_Number"],
                        FullName = reader.IsDBNull(reader.GetOrdinal("Full_Name")) ? (string?)null : (string)reader["Full_Name"],
                        ForceChangePassword = (bool)reader["Force_Change_Password"],
                        PrefferredLanguage = reader.IsDBNull(reader.GetOrdinal("Prefferred_Language")) ? (string?)null : (string)reader["Prefferred_Language"]
                    });
                }

                cnx.Close();
                return allLogins;
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Delete from Security_Logins
                                               where Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Update Security_Logins
                                                 set Login = @Login,
                                                     Password = @Password,
                                                     Created_Date = @Created_Date,
                                                     Password_Update_Date = @Password_Update_Date,
                                                     Agreement_Accepted_Date = @Agreement_Accepted_Date,
                                                     Is_Locked = @Is_Locked,
                                                     Is_Inactive = @Is_Inactive,
                                                     Email_Address = @Email_Address,
                                                     Phone_Number = @Phone_Number,
                                                     Full_Name = @Full_Name,
                                                     Force_Change_Password = @Force_Change_Password,
                                                     Prefferred_Language = @Prefferred_Language
                                               where ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Password", poco.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", poco.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", poco.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", poco.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", poco.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", poco.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", poco.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }
    }
}
