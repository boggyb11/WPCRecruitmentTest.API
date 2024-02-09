using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using WPCRecruitmentTest.Models;
using WPCRecruitmentTest.Models.Enums;
using WPCRecruitmentTest.Services.Interfaces;
using WPCRecruitmentTest.Services.Services;
using WPCRecruitmentTest.ViewModels.Requests;
using WPCRecruitmentTest.ViewModels.Responses;

namespace WPCRecruitmentTest.Test
{
    public class CrimeServiceTests
    {
        [Fact]
        public async Task GetCrimes_Returns_CrimeDataSortedByCategory()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config.GetSection("Urls:PoliceApiUrl").Value).Returns("dummy-url");
            var dateHelperMock = new Mock<DateHelper>();
            var locationHelperMock = new Mock<LocationHelper>();
            var apiCallerMock = new Mock<IApiCaller>();

            var service = new CrimeService(configurationMock.Object, dateHelperMock.Object, apiCallerMock.Object, locationHelperMock.Object);

            var request = new GetCrimesRequest
            {
                Lat = 51.509865f,
                Lng = -0.118092f,
                Month = Month.January
            };

            var mockResponse = JsonConvert.SerializeObject(new List<CrimeData>
            {
                new CrimeData { Category = "Burglary" },
                new CrimeData { Category = "Assault" },
                new CrimeData { Category = "Theft" }
            });

            apiCallerMock.Setup(api => api.SendGetRequestAsync(It.IsAny<string>())).ReturnsAsync(mockResponse);

            // Act
            var result = await service.GetCrimes(request);

            // Assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Content);

            var viewModel = result.Content;
            var expectedCategories = new List<string> { "Assault", "Burglary", "Theft" }; //Ensure they are sorted by category
            Assert.Equal(expectedCategories, viewModel.CrimeData.Select(c => c.Category).ToList()); // Ensure correct categories
        }

        [Fact]
        public async Task GetCrimes_Returns_CrimeDataCount()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config.GetSection("Urls:PoliceApiUrl").Value).Returns("dummy-url");
            var dateHelperMock = new Mock<DateHelper>();
            var locationHelperMock = new Mock<LocationHelper>();
            var apiCallerMock = new Mock<IApiCaller>();

            var service = new CrimeService(configurationMock.Object, dateHelperMock.Object, apiCallerMock.Object, locationHelperMock.Object);

            var request = new GetCrimesRequest
            {
                Lat = 51.509865f,
                Lng = -0.118092f,
                Month = Month.January
            };

            var mockResponse = JsonConvert.SerializeObject(new List<CrimeData>
            {
                new CrimeData { Category = "Burglary" },
                new CrimeData { Category = "Assault" },
                new CrimeData { Category = "Theft" }
            });

            apiCallerMock.Setup(api => api.SendGetRequestAsync(It.IsAny<string>())).ReturnsAsync(mockResponse);

            // Act
            var result = await service.GetCrimes(request);

            // Assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Content);

            var viewModel = result.Content;
            Assert.Equal(3, viewModel.CrimeCount); // Ensure correct number of crimes
        }

        [Fact]
        public async Task GetCrimes_Returns_Error_If_OutsideUK()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config.GetSection("Urls:PoliceApiUrl").Value).Returns("dummy-url");
            var dateHelperMock = new Mock<DateHelper>();
            var locationHelperMock = new Mock<LocationHelper>();
            var apiCallerMock = new Mock<IApiCaller>();

            var service = new CrimeService(configurationMock.Object, dateHelperMock.Object, apiCallerMock.Object, locationHelperMock.Object);

            var request = new GetCrimesRequest
            {
                Lat = 65.509865f,// Outside UK
                Lng = -0.118092f,
                Month = Month.January
            };

            var mockResponse = JsonConvert.SerializeObject(new List<CrimeData>
            {
                new CrimeData { Category = "Burglary" },
                new CrimeData { Category = "Assault" },
                new CrimeData { Category = "Theft" }
            });

            apiCallerMock.Setup(api => api.SendGetRequestAsync(It.IsAny<string>())).ReturnsAsync(mockResponse);

            // Act
            var result = await service.GetCrimes(request);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal(ErrorMessages.Generic.NotWithinUK, result.Error);
        }

        [Fact]
        public async Task GetCrimes_Returns_Failure_If_ApiCallFails()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(config => config.GetSection("Urls:PoliceApiUrl").Value).Returns("dummy-url");
            var dateHelperMock = new Mock<DateHelper>();
            var locationHelperMock = new Mock<LocationHelper>();
            var apiCallerMock = new Mock<IApiCaller>();

            var service = new CrimeService(configurationMock.Object, dateHelperMock.Object, apiCallerMock.Object, locationHelperMock.Object);

            var request = new GetCrimesRequest
            {
                Lat = 51.509865f,
                Lng = -0.118092f,
                Month = Month.January
            };

            apiCallerMock.Setup(api => api.SendGetRequestAsync(It.IsAny<string>())).ReturnsAsync((string)null); // Simulate API call failure

            // Act
            var result = await service.GetCrimes(request);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal(ErrorMessages.Generic.SomethingWentWrong, result.Error);
        }
    }
}
