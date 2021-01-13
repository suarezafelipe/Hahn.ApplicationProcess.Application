using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Entities;

namespace Hahn.ApplicationProcess.December2020.Domain.Interfaces
{
    public interface IApplicantRepository
    {
        Task<Applicant> GetAsync(int id);
        Task<Applicant> Create(Applicant applicant);
        Task<bool> Delete(int id);
        Task<bool> Update(Applicant applicant);
    }
}
