using FluentValidation;
using FluentValidation.Validators;
using Hahn.ApplicationProcess.December2020.Domain.Entities;

namespace Hahn.ApplicationProcess.December2020.Domain.Validators
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(x => x.Name).MinimumLength(5);
            RuleFor(x => x.FamilyName).MinimumLength(5);
            RuleFor(x => x.Address).MinimumLength(10);

            // CountryOfOrigin – must be a valid Country – therefore ask with an httpclient here https://restcountries.eu/rest/v2/… – ApiDescription: https://restcountries.eu/#api-endpoints-full-name if the country is found, the country is valid.

            RuleFor(x => x.EmailAddress).EmailAddress(EmailValidationMode.Net4xRegex);
            RuleFor(x => x.Age).InclusiveBetween(20, 60);
            RuleFor(x => x.Hired).NotNull();
        }
    }
}
