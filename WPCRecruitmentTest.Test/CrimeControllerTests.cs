using Microsoft.AspNetCore.Mvc;
using Moq;
using WPCRecruitmentTest.API.Controllers;
using WPCRecruitmentTest.Models;
using WPCRecruitmentTest.Services.Interfaces;
using WPCRecruitmentTest.ViewModels.Requests;
using WPCRecruitmentTest.ViewModels.Responses;

namespace WPCRecruitmentTest.Test
{
    public class CrimeControllerTests
    {
        [Fact]
        public async Task GetCrimes_ValidRequest_ReturnsOk()
        {
            // Arrange
            var crimeServiceMock = new Mock<ICrimeService>();
            var controller = new CrimeController(crimeServiceMock.Object);
            var request = new GetCrimesRequest();

            var expectedResult = new CrimeViewModel();

            crimeServiceMock.Setup(service => service.GetCrimes(request))
                            .ReturnsAsync(new CommandResult<CrimeViewModel> { Succeeded = true, Content = expectedResult });

            // Act
            var result = await controller.GetCrimes(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<CrimeViewModel>(okResult.Value);
            Assert.Equal(expectedResult, model);
        }

        [Fact]
        public async Task GetCrimes_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var crimeServiceMock = new Mock<ICrimeService>();
            var controller = new CrimeController(crimeServiceMock.Object);
            var request = new GetCrimesRequest();
            controller.ModelState.AddModelError("PropertyName", "Error message");

            // Act
            var result = await controller.GetCrimes(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var modelStateErrors = controller.ModelState["PropertyName"].Errors;

            Assert.Single(modelStateErrors);
            Assert.Contains("Error message", modelStateErrors.Select(e => e.ErrorMessage));
        }

        [Fact]
        public async Task GetCrimes_ServiceReturnsError_ReturnsBadRequest()
        {
            // Arrange
            var crimeServiceMock = new Mock<ICrimeService>();
            var controller = new CrimeController(crimeServiceMock.Object);
            var request = new GetCrimesRequest();

            var errorMessage = ErrorMessages.Generic.SomethingWentWrong;
            crimeServiceMock.Setup(service => service.GetCrimes(request))
                            .ReturnsAsync(new CommandResult<CrimeViewModel> { Succeeded = false, Error = errorMessage });

            // Act
            var result = await controller.GetCrimes(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var error = Assert.IsType<Error>(badRequestResult.Value);
            Assert.Equal(errorMessage, error);
        }
    }
}
