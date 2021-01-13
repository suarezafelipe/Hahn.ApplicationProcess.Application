namespace Hahn.ApplicationProcess.December2020.Domain.Entities
{
    /// <summary>
    /// Applicant entity
    /// </summary>
    public class Applicant
    {
        public int Id { get; set; }

        /// <example>Felipe</example>
        public string Name { get; set; }

        /// <example>Suarez</example>
        public string FamilyName { get; set; }

        /// <example>Friedhofstraße 11 93142 Maxhütte-Haidhof</example>
        public string Address { get; set; }

        /// <example>Germany</example>
        public string CountryOfOrigin { get; set; }

        /// <example>test@gmail.com</example>
        public string EmailAddress { get; set; }

        /// <example>31</example>
        public int Age { get; set; }

        /// <example>true</example>
        public bool Hired { get; set; }
    }
}
