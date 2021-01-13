using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.December2020.Data.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
        private readonly AppDbContext _db;

        public ApplicantRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Applicant> GetAsync(int id)
        {
            return await _db.Applicants.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Applicant> Create(Applicant applicant)
        {
            await _db.Applicants.AddAsync(applicant);
            await _db.SaveChangesAsync();
            return applicant;
        }
    }
}
