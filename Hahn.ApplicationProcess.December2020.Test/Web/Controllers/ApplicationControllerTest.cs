using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Interfaces;
using Hahn.ApplicationProcess.December2020.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Hahn.ApplicationProcess.December2020.Test.Web.Controllers
{
    public class ApplicationControllerTest
    {
        private readonly ApplicantController _sut;
        private readonly Mock<IApplicantService> _applicantServiceMock = new();

        public ApplicationControllerTest()
        {
            var request = new Mock<HttpRequest>();
            request.Setup(x => x.Scheme).Returns("http");
            request.Setup(x => x.Host).Returns(HostString.FromUriComponent("http://localhost"));
            request.Setup(x => x.PathBase).Returns(PathString.FromUriComponent("/applicant"));

            var httpContext = Mock.Of<HttpContext>(_ =>
                _.Request == request.Object
            );


            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            _sut = new ApplicantController(_applicantServiceMock.Object)
            {
                ControllerContext = controllerContext
            };
        }

        [Fact]
        public async Task GetAsync_ShouldReturnApplicant_WhenApplicantExists()
        {
            // Arrange
            var applicantId = 1;
            var mockedApplicant = MockValidApplicant();
            mockedApplicant.Id = applicantId;

            _applicantServiceMock.Setup(x => x.GetAsync(applicantId)).ReturnsAsync(mockedApplicant);

            // Act
            var controllerResult = await _sut.GetAsync(applicantId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(controllerResult);
            var model = Assert.IsType<Applicant>(okResult.Value);
            Assert.Equal(applicantId, model.Id);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNotFound_WhenApplicantDoesNotExists()
        {
            // Arrange
            _applicantServiceMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var controllerResult = await _sut.GetAsync(It.IsAny<int>());

            // Assert
            Assert.IsType<NotFoundResult>(controllerResult);
        }

        [Fact]
        public async Task PostAsync_ShouldReturnCreatedApplicant_WhenApplicantModelIsValid()
        {
            // Arrange
            var requestApplicant = MockValidApplicant();
            var applicantId = 1;
            var responseApplicant = MockValidApplicant();
            responseApplicant.Id = applicantId;

            _applicantServiceMock.Setup(x => x.CreateAsync(requestApplicant)).ReturnsAsync(responseApplicant);

            // Act
            var controllerResult = await _sut.PostAsync(requestApplicant);

            // Assert
            var okResult = Assert.IsType<CreatedResult>(controllerResult);
            var model = Assert.IsType<Applicant>(okResult.Value);
            Assert.Equal(applicantId, model.Id);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnOk_WhenApplicantExists()
        {
            // Arrange
            var requestApplicant = MockValidApplicant();
            _applicantServiceMock.Setup(x => x.UpdateAsync(requestApplicant)).ReturnsAsync(true);

            // Act
            var controllerResult = await _sut.PutAsync(requestApplicant);

            // Assert
            Assert.IsType<OkResult>(controllerResult);
        }

        [Fact]
        public async Task PutAsync_ShouldReturnNoContent_WhenApplicantDoesNotExist()
        {
            // Arrange
            var requestApplicant = MockValidApplicant();
            _applicantServiceMock.Setup(x => x.UpdateAsync(requestApplicant)).ReturnsAsync(false);

            // Act
            var controllerResult = await _sut.PutAsync(requestApplicant);

            // Assert
            Assert.IsType<NoContentResult>(controllerResult);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnNoContent_WhenApplicantDoesNotExist()
        {
            // Arrange
            var applicantId = 1;
            _applicantServiceMock.Setup(x => x.DeleteAsync(applicantId)).ReturnsAsync(false);

            // Act
            var controllerResult = await _sut.DeleteAsync(applicantId);

            // Assert
            Assert.IsType<NoContentResult>(controllerResult);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnOk_WhenApplicantExists()
        {
            // Arrange
            var applicantId = 1;
            _applicantServiceMock.Setup(x => x.DeleteAsync(applicantId)).ReturnsAsync(true);

            // Act
            var controllerResult = await _sut.DeleteAsync(applicantId);

            // Assert
            Assert.IsType<OkResult>(controllerResult);
        }

        private static Applicant MockValidApplicant()
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