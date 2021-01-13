using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Entities;

namespace Hahn.ApplicationProcess.December2020.Domain.Interfaces
{
    public interface IApplicantService
    {
        Task<Applicant> GetAsync(int id);
    }
}
