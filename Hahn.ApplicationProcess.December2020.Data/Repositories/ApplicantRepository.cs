﻿using System.Threading.Tasks;
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
            return await _db.Applicants.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Applicant> CreateAsync(Applicant applicant)
        {
            await _db.Applicants.AddAsync(applicant);
            await _db.SaveChangesAsync();
            return applicant;
        }

        public async Task<bool> UpdateAsync(Applicant applicant)
        {
            _db.Update(applicant);
            var changedRows = await _db.SaveChangesAsync();
            return changedRows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var applicantToRemove = await GetAsync(id);

            if (applicantToRemove == null)
                return false;

            _db.Applicants.Remove(applicantToRemove);
            var changedRows = await _db.SaveChangesAsync();
            return changedRows > 0;
        }
    }
}