using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Interfaces;

namespace Hahn.ApplicationProcess.December2020.Domain.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IApplicantRepository _applicantRepository;

        public ApplicantService(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public async Task<Applicant> GetAsync(int id)
        {
            return await _applicantRepository.GetAsync(id);
        }

        public async Task<Applicant> CreateAsync(Applicant applicant)
        {
            return await _applicantRepository.CreateAsync(applicant);
        }

        public async Task<bool> UpdateAsync(Applicant applicant)
        {
            var exists = await _applicantRepository.GetAsync(applicant.Id);

            if (exists == null)
                return false;

            return await _applicantRepository.UpdateAsync(applicant);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _applicantRepository.DeleteAsync(id);
        }
    }
}
