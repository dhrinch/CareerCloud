using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{    
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        private readonly string connectionString = Config.GetConnectionString();
        public void Add(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items) 
                { 
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Insert into Applicant_Educations
                                                   (Id,
                                                    Applicant,
                                                    Major,
                                                    Certificate_Diploma,
                                                    Start_Date,
                                                    Completion_Date,
                                                    Completion_Percent)
                                            values
                                                   (@Id,
                                                    @Applicant,
                                                    @Major,
                                                    @Certificate_Diploma,
                                                    @Start_Date,
                                                    @Completion_Date,
                                                    @Completion_Percent)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

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

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnx;
                SqlDataReader reader = (new SqlCommand("Select * from Applicant_Educations", cnx)).ExecuteReader();
                     
                var allApplicants = new List <ApplicantEducationPoco>();
                while (reader.Read())                
                {
                    allApplicants.Add(new ApplicantEducationPoco()
                    {
                            Id = (Guid)reader["Id"],
                            Applicant = (Guid)reader["Applicant"],
                            Major = (string)reader["Major"],                            
                            CertificateDiploma = reader.IsDBNull(reader.GetOrdinal("Certificate_Diploma")) ? (string?)null : (string)reader["Certificate_Diploma"],                        
                            StartDate = reader.IsDBNull(reader.GetOrdinal("Start_Date")) ? (DateTime?)null : (DateTime)reader["Start_Date"],                            
                            CompletionDate = reader.IsDBNull(reader.GetOrdinal("Completion_Date")) ? (DateTime?)null : (DateTime)reader["Completion_Date"],                            
                            CompletionPercent = reader.IsDBNull(reader.GetOrdinal("Completion_Percent")) ? (byte?)null : (byte)reader["Completion_Percent"]
                    });
                }                

                cnx.Close();
                return allApplicants;
            }
         }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {                
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Delete from Applicant_Educations
                                               where Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Update Applicant_Educations
                                               set Applicant = @Applicant,
                                                   Major = @Major,
                                                   Certificate_Diploma = @Certificate_Diploma,
                                                   Start_Date = @Start_Date,
                                                   Completion_Date = @Completion_Date,
                                                   Completion_Percent  = @Completion_Percent
                                               where ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }
    }
}
