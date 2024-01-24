using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService: ApplicantEducation.ApplicantEducationBase
    {   
        private readonly ApplicantEducationLogic applicantEducationLogic;
        public ApplicantEducationService()
        {
            var repo = new EFGenericRepository<ApplicantEducationPoco>();
            _applicantEducationLogic = new ApplicantEducationLogic(repo);
        }
        public override Task<ApplicantEducationReply> GetApplicantEducation(IdRequestApplicantEducation request, ServerCallContext context)
        {
            var applicantEducation = _applicantEducationLogic.Get(Guid.Parse(request.Id));
            return Task.FromResult();
        }

        private ApplicantEducationReply FromPoco(ApplicantEducationPoco applicantEducationPoco)
        {
            return new ApplicationEducationReply()
            {
                Id = applicantEducationPoco.Id.ToString(),
                Applicant = applicantEducationPoco.Applicant.ToString();
                Major=applicantEducationPoco.Major;
                CertificateDiploma = applicantEducationPoco.CertificateDiploma;
                StartDate=applicantEducationPoco.StartDate ?? TimeStamp.From;
            }
        }
    }
}
