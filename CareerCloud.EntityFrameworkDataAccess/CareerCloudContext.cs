using CareerCloud.Pocos;
using CareerCloud.ADODataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        private readonly string _connectionString;

        public CareerCloudContext()
        {
            _connectionString = Config.GetConnectionString() + "; Trusted_Connection=true; Encrypt=False;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }        
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Applicant connections
            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(e => e.SystemCountryCode)
                .WithMany(e => e.ApplicantProfiles)
                .HasForeignKey(e => e.Country);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(e => e.SecurityLogin)
                .WithMany(e => e.ApplicantProfiles)
                .HasForeignKey(e => e.Login);

            modelBuilder.Entity<ApplicantResumePoco>()
                .HasOne(e => e.ApplicantProfile)
                .WithMany(e => e.ApplicantResumes)
                .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(e => e.ApplicantProfile)
                .WithMany(e => e.ApplicantJobApplications)
                .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(e => e.CompanyJob)
                .WithMany(e => e.ApplicantJobApplications)
                .HasForeignKey(e => e.Job);            

            modelBuilder.Entity<ApplicantSkillPoco>()
                .HasOne(e => e.ApplicantProfile)
                .WithMany(e => e.ApplicantSkills)
                .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<ApplicantEducationPoco>()
                .HasOne(e => e.ApplicantProfile)
                .WithMany(e => e.ApplicantEducations)
                .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(e => e.ApplicantProfile)
                .WithMany(e => e.ApplicantWorkHistorys)
                .HasForeignKey(e => e.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(e => e.SystemCountryCode)
                .WithMany(e => e.ApplicantWorkHistories)
                .HasForeignKey(e => e.CountryCode);
            #endregion

            #region Company Connections
            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(e => e.CompanyProfile)
                .WithMany(e => e.CompanyDescriptions)
                .HasForeignKey(e => e.Company);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(e => e.SystemLanguageCode)
                .WithMany(e => e.CompanyDescriptions)
                .HasForeignKey(e => e.LanguageId);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(e => e.CompanyProfile)
                .WithMany(e => e.CompanyLocations)
                .HasForeignKey(e => e.Company);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(e => e.SystemCountryCode)
                .WithMany(e => e.CompanyLocations)
                .HasForeignKey(e => e.CountryCode);
            #endregion

            #region CompanyJob Connections
            modelBuilder.Entity<CompanyJobPoco>()
                .HasOne(e => e.CompanyProfile)
                .WithMany(e => e.CompanyJobs)
                .HasForeignKey(e => e.Company);

            modelBuilder.Entity<CompanyJobEducationPoco>()
                .HasOne(e => e.CompanyJob)
                .WithMany(e => e.CompanyJobEducations)
                .HasForeignKey(e => e.Job);

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .HasOne(e => e.CompanyJob)
                .WithMany(e => e.CompanyJobSkills)
                .HasForeignKey(e => e.Job);

            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .HasOne(e => e.CompanyJob)
                .WithMany(e => e.CompanyJobDescriptions)
                .HasForeignKey(e => e.Job);
            #endregion

            #region Login Connections
            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(e => e.SecurityRole)
                .WithMany(e => e.SecurityLoginsRoles)
                .HasForeignKey(e => e.Role);

            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .HasOne(e => e.SecurityLogin)
                .WithMany(e => e.SecurityLoginsLogs)
                .HasForeignKey(e => e.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(e => e.SecurityLogin)
                .WithMany(e => e.SecurityLoginsRoles)
                .HasForeignKey(e => e.Login);
            #endregion

            #region TimeStamp Fix
            modelBuilder.Entity<ApplicantProfilePoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<ApplicantSkillPoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<ApplicantEducationPoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyJobPoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyJobEducationPoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyLocationPoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<CompanyProfilePoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();

            modelBuilder.Entity<SecurityLoginPoco>()
                .Property(e => e.TimeStamp)
                .IsRowVersion();
            #endregion
        }
    }
}
