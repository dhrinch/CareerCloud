using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        private readonly string connectionString = Config.GetConnectionString();
        public void Add(params ApplicantResumePoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Insert into Applicant_Resumes
                                                   (Id,
                                                    Applicant,
                                                    Resume,
                                                    Last_Updated)
                                            values
                                                   (@Id,
                                                    @Applicant,
                                                    @Resume,
                                                    @Last_Updated)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);

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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                var cmd = new SqlCommand();
                cmd.Connection = cnx;
                SqlDataReader reader = (new SqlCommand("Select * from Applicant_Resumes", cnx)).ExecuteReader();

                var allResumes = new List<ApplicantResumePoco>();
                while (reader.Read())
                
                {
                    allResumes.Add(new ApplicantResumePoco()
                    {
                        Id = (Guid)reader["Id"],
                        Applicant = (Guid)reader["Applicant"],
                        Resume = (string)reader["Resume"],
                        LastUpdated = reader.IsDBNull(reader.GetOrdinal("Last_Updated")) ? (DateTime?)null : (DateTime)reader["Last_Updated"]
                    });
                }
                cnx.Close();
                return allResumes;
            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Delete from Applicant_Resumes
                                       where Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (SqlConnection cnx = new SqlConnection(connectionString))
            {
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = cnx;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"Update Applicant_Resumes
                                               set Applicant = @Applicant,
                                                   Resume = @Resume,
                                                   Last_Updated = @Last_Updated
                                               where ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
                cnx.Close();
            }
        }
    }
}
