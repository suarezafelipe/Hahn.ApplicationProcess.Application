using AutoFixture;
using FluentValidation.TestHelper;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Validators;
using Xunit;

namespace Hahn.ApplicationProcess.December2020.Test.Domain.Validators
{
    public class ApplicantValidatorTest
    {
        private readonly ApplicantValidator _sut;
        private readonly Fixture _fixture = new();

        public ApplicantValidatorTest()
        {
            _sut = new ApplicantValidator();
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("aa")]
        [InlineData("")]
        public void ApplicantValidator_ShouldReturnError_WhenNameIsShorterThanFiveChars(string name)
        {
            var applicant = _fixture.Create<Applicant>();
            applicant.Name = name;

            var validationResult = _sut.TestValidate(applicant);

            validationResult.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("aa")]
        [InlineData("")]
        public void ApplicantValidator_ShouldReturnError_WhenFamilyNameIsShorterThanFiveChars(string familyName)
        {
            var applicant = _fixture.Create<Applicant>();
            applicant.FamilyName = familyName;

            var validationResult = _sut.TestValidate(applicant);

            validationResult.ShouldHaveValidationErrorFor(x => x.FamilyName);
        }

        [Theory]
        [InlineData("street 1")]
        [InlineData("12 straße")]
        [InlineData("")]
        public void ApplicantValidator_ShouldReturnError_WhenAddressIsShorterThanTenChars(string address)
        {
            var applicant = _fixture.Create<Applicant>();
            applicant.Address = address;

            var validationResult = _sut.TestValidate(applicant);


            validationResult.ShouldHaveValidationErrorFor(x => x.Address);
        }

        [Theory]
        [InlineData("Deutschland")]
        [InlineData("Kolumbien")]
        public void ApplicantValidator_ShouldReturnError_WhenCountryOfOriginIsNotInEnglish(string countryOfOrigin)
        {
            var applicant = _fixture.Create<Applicant>();
            applicant.CountryOfOrigin = countryOfOrigin;

            var validationResult = _sut.TestValidate(applicant);

            validationResult.ShouldHaveValidationErrorFor(x => x.CountryOfOrigin);
        }

        [Theory]
        [InlineData("Germany")]
        [InlineData("DE")]
        [InlineData("Colombia")]
        [InlineData("CO")]
        public void ApplicantValidator_ShouldNotReturnError_WhenCountryOfOriginIsValid(string countryOfOrigin)
        {
            var applicant = _fixture.Create<Applicant>();
            applicant.CountryOfOrigin = countryOfOrigin;

            var validationResult = _sut.TestValidate(applicant);

            validationResult.ShouldNotHaveValidationErrorFor(x => x.CountryOfOrigin);
        }

        [Theory]
        [InlineData("not-an-email")]
        [InlineData("no@domain")]
        [InlineData(".com@disordered")]
        public void ApplicantValidator_ShouldReturnError_WhenEmailAddressIsNotValid(string emailAddress)
        {
            var applicant = _fixture.Create<Applicant>();
            applicant.EmailAddress = emailAddress;

            var validationResult = _sut.TestValidate(applicant);

            validationResult.ShouldHaveValidationErrorFor(x => x.EmailAddress);
        }

        [Theory]
        [InlineData("test@gmail.com")]
        [InlineData("a@a.co")]
        [InlineData("t@t.tv")]
        public void ApplicantValidator_ShouldNotReturnError_WhenEmailAddressIsValid(string emailAddress)
        {
            var applicant = _fixture.Create<Applicant>();
            applicant.EmailAddress = emailAddress;

            var validationResult = _sut.TestValidate(applicant);

            validationResult.ShouldNotHaveValidationErrorFor(x => x.EmailAddress);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(70)]
        [InlineData(0)]
        public void ApplicantValidator_ShouldReturnError_WhenAgeIsLessThan20OrGreaterThan60(int age)
        {
            var applicant = _fixture.Create<Applicant>();
            applicant.Age = age;

            var validationResult = _sut.TestValidate(applicant);

            validationResult.ShouldHaveValidationErrorFor(x => x.Age);
        }
    }
}
