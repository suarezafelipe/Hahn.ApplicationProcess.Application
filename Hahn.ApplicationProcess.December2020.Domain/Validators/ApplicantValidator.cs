using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using RestSharp;

namespace Hahn.ApplicationProcess.December2020.Domain.Validators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(x => x.Name).MinimumLength(5);
            RuleFor(x => x.FamilyName).MinimumLength(5);
            RuleFor(x => x.Address).MinimumLength(10);
            RuleFor(x => x.Age).InclusiveBetween(20, 60);
            RuleFor(x => x.Hired).NotNull();

            // This validation was asked in the requirements, so we are using Net4xRegex validation mode even though is deprecated
            RuleFor(x => x.EmailAddress).EmailAddress(EmailValidationMode.Net4xRegex);

            RuleFor(r => r.CountryOfOrigin).Must(ValidateCountry)
                .WithMessage("CountryOfOrigin not found. Try with the english name or with the 2 letter ISO code");
        }

        private static bool ValidateCountry(Applicant applicant, string country)
        {
            RestClient client = new("https://restcountries.eu");
            RestRequest request = new($"/rest/v2/name/{country}?fullText=true");

            var apiResponse = client.GetAsync<List<CountriesApiModel>>(request).GetAwaiter().GetResult();

            // If the api response was not null or empty it means the country was found and we return "true" meaning it was valid.
            return !string.IsNullOrEmpty(apiResponse.FirstOrDefault()?.Name);
        }
    }

    public class CountriesApiModel
    {
        public string Name { get; set; }
    }
}