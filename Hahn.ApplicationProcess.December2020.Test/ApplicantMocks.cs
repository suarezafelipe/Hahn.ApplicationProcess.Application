using Hahn.ApplicationProcess.December2020.Domain.Entities;

namespace Hahn.ApplicationProcess.December2020.Test
{
    public static class ApplicantMocks
    {
        public static Applicant MockValidApplicant()
        {
            return new()
            {
                Name = "Felipe Test",
                FamilyName = "Suarez Test",
                Address = "CL 55 14 47 APT 222",
                Age = 31,
                CountryOfOrigin = "CO",
                EmailAddress = "felipe@gmail.com",
                Hired = true
            };
        }
    }
}
