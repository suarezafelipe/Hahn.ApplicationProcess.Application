using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Interfaces;

namespace Hahn.ApplicationProcess.December2020.Data.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {
        public async Task<Applicant> GetAsync(int id)
        {
            var applicants = new List<Applicant>
            {
                new Applicant
                {
                    Address = "123 st.",
                    Age = 30,
                    CountryOfOrigin = "CO",
                    EmailAddress = "a@a.co",
                    FamilyName = "Suarez",
                    Hired = true,
                    Id = 1,
                    Name = "Andres"
                },
                new Applicant
                {
                    Address = "123 st.",
                    Age = 30,
                    CountryOfOrigin = "CO",
                    EmailAddress = "b@b.co",
                    FamilyName = "Rodriguez",
                    Hired = true,
                    Id = 2,
                    Name = "Carolina"
                }
            };

            return applicants.FirstOrDefault(x => x.Id == id);
        }

        public async Task<Applicant> Create(Applicant applicant)
        {
            applicant.Id = 3;
            return applicant;
        }
    }
}
