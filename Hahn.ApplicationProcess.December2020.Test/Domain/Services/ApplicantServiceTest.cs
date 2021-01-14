using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Domain.Interfaces;
using Hahn.ApplicationProcess.December2020.Domain.Services;
using Moq;
using Xunit;

namespace Hahn.ApplicationProcess.December2020.Test.Domain.Services
{
    public class ApplicantServiceTest
    {
        private readonly ApplicantService _sut;
        private readonly Mock<IApplicantRepository> _applicantRepositoryMock = new();

        public ApplicantServiceTest()
        {
            _sut = new ApplicantService(_applicantRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnApplicant_WhenApplicantExists()
        {
            // Arrange
            const int applicantId = 1;
            var mockedApplicant = ApplicantMocks.MockValidApplicant();
            mockedApplicant.Id = applicantId;

            _applicantRepositoryMock.Setup(x => x.GetAsync(applicantId)).ReturnsAsync(mockedApplicant);

            // Act
            var serviceResult = await _sut.GetAsync(applicantId);

            // Assert
            Assert.Equal(applicantId, serviceResult.Id);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenApplicantDoesNotExist()
        {
            // Arrange
            _applicantRepositoryMock.Setup(x => x.GetAsync(It.IsAny<int>())).ReturnsAsync(() => null);

            // Act
            var serviceResult = await _sut.GetAsync(It.IsAny<int>());

            // Assert
            Assert.Null(serviceResult);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnCreatedApplicant_WhenApplicantModelIsValid()
        {
            // Arrange
            var requestApplicant = ApplicantMocks.MockValidApplicant();
            const int applicantId = 1;
            var responseApplicant = ApplicantMocks.MockValidApplicant();
            responseApplicant.Id = applicantId;

            _applicantRepositoryMock.Setup(x => x.CreateAsync(requestApplicant)).ReturnsAsync(responseApplicant);

            // Act
            var serviceResult = await _sut.CreateAsync(requestApplicant);

            // Assert
            Assert.Equal(applicantId, serviceResult.Id);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenApplicantExistsAndWasUpdated()
        {
            // Arrange
            var requestApplicant = ApplicantMocks.MockValidApplicant();

            _applicantRepositoryMock.Setup(x => x.GetAsync(requestApplicant.Id)).ReturnsAsync(requestApplicant);
            _applicantRepositoryMock.Setup(x => x.UpdateAsync(requestApplicant)).ReturnsAsync(true);

            // Act
            var serviceResult = await _sut.UpdateAsync(requestApplicant);

            // Assert
            Assert.True(serviceResult);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenApplicantDoesNotExist()
        {
            // Arrange
            var requestApplicant = ApplicantMocks.MockValidApplicant();

            _applicantRepositoryMock.Setup(x => x.GetAsync(requestApplicant.Id)).ReturnsAsync(() => null);

            // Act
            var serviceResult = await _sut.UpdateAsync(requestApplicant);

            // Assert
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenApplicantCouldNotBeUpdated()
        {
            // Arrange
            var requestApplicant = ApplicantMocks.MockValidApplicant();

            _applicantRepositoryMock.Setup(x => x.GetAsync(requestApplicant.Id)).ReturnsAsync(requestApplicant);
            _applicantRepositoryMock.Setup(x => x.UpdateAsync(requestApplicant)).ReturnsAsync(false);

            // Act
            var serviceResult = await _sut.UpdateAsync(requestApplicant);

            // Assert
            Assert.False(serviceResult);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenApplicantWasDeletedSuccessfully()
        {
            // Arrange
            const int applicantId = 1;
            _applicantRepositoryMock.Setup(x => x.DeleteAsync(applicantId)).ReturnsAsync(true);

            // Act
            var serviceResult = await _sut.DeleteAsync(applicantId);

            // Assert
            Assert.True(serviceResult);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenApplicantCouldNotBeDeleted()
        {
            // Arrange
            const int applicantId = 1;
            _applicantRepositoryMock.Setup(x => x.DeleteAsync(applicantId)).ReturnsAsync(false);

            // Act
            var serviceResult = await _sut.DeleteAsync(applicantId);

            // Assert
            Assert.False(serviceResult);
        }
    }
}