using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAPI.Models;
using TestAPI.Repositories;
using TestAPI.Services;
using Xunit;

namespace TestAPIUnitTest.Services
{
    public class CarLocationServiceUnitTest
    {
        private readonly Mock<IBaseRepository<CarLocation>> _mockCarLocationRepository;

        public CarLocationServiceUnitTest()
        {
            _mockCarLocationRepository = new Mock<IBaseRepository<CarLocation>>();
        }

        [Fact]
        public async Task SearchCarLocationsAsync_ShouldBe_Success_ToReturnList()
        {
            // Arrange
            List<CarLocation> carLocations = new List<CarLocation>();
            for (int i = 1; i <= 5; i++)
            {
                carLocations.Add(new CarLocation
                {
                    CarDriverId = i,
                    Longitude = i,
                    Latitude = i,
                });
            }

            _mockCarLocationRepository.Setup(a => a.ListAsync()).Returns(Task.FromResult(carLocations));

            var carLocationService = new CarLocationService(_mockCarLocationRepository.Object);


            // Act
            var response = await carLocationService.GetCarLocationsAsync();


            // Assert
            Assert.NotNull(response.Data);
        }

        [Theory]
        [InlineData(1, 2)]
        public async Task SearchCarLocationsAsync_ShouldBe_Fail_DueToCarLocationNotAvailable(double userLongitude, double userLatitude)
        {
            // Arrange
            List<CarLocation> carLocations = new List<CarLocation>();
            _mockCarLocationRepository.Setup(a => a.ListAsync()).Returns(Task.FromResult(carLocations));

            var carLocationService = new CarLocationService(_mockCarLocationRepository.Object);


            // Act
            var response = await carLocationService.SearchCarLocationsAsync(userLongitude, userLatitude);


            // Assert
            Assert.NotNull(response.Message);
        }

        [Theory]
        [InlineData(6, 6)]
        public async Task SearchCarLocationsAsync_ShouldBe_Fail_DueToNearestCarUnavailable(double userLongitude, double userLatitude)
        {
            // Arrange
            List<CarLocation> carLocations = new List<CarLocation>();
            for (int i = 1; i <= 5; i++)
            {
                carLocations.Add(new CarLocation
                {
                    CarDriverId = i,
                    Longitude = i,
                    Latitude = i,
                });
            }

            _mockCarLocationRepository.Setup(a => a.ListAsync()).Returns(Task.FromResult(carLocations));

            var carLocationService = new CarLocationService(_mockCarLocationRepository.Object);


            // Act
            var response = await carLocationService.SearchCarLocationsAsync(userLongitude, userLatitude);


            // Assert
            Assert.NotNull(response.Message);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(4, 2)]
        [InlineData(5, 2)]
        public async Task SearchCarLocationsAsync_ShouldBe_Available(double userLongitude, double userLatitude)
        {
            // Arrange
            List<CarLocation> carLocations = new List<CarLocation>();
            for (int i = 1; i <= 5; i++)
            {
                carLocations.Add(new CarLocation
                {
                    CarDriverId = i,
                    Longitude = i,
                    Latitude = i,
                });
            }

            _mockCarLocationRepository.Setup(a => a.ListAsync()).Returns(Task.FromResult(carLocations));

            var carLocationService = new CarLocationService(_mockCarLocationRepository.Object);


            // Act
            var response = await carLocationService.SearchCarLocationsAsync(userLongitude, userLatitude);
            var carLocationCount = ((List<CarLocation>)response.Data).Count;


            // Assert
            Assert.NotNull(response.Data);
            Assert.True(carLocationCount > 0 && carLocationCount < 2);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 2)]
        [InlineData(4, 2)]
        [InlineData(5, 2)]
        public async Task SearchCarLocationsAsync_ShouldNotBe_Available(double userLongitude, double userLatitude)
        {
            // Arrange
            List<CarLocation> carLocations = new List<CarLocation>();
            for (int i = 1; i <= 5; i++)
            {
                carLocations.Add(new CarLocation
                {
                    CarDriverId = i,
                    Longitude = new Random().Next(-2, 2),
                    Latitude = new Random().Next(-2, 2)
                });
            }

            _mockCarLocationRepository.Setup(a => a.ListAsync()).Returns(Task.FromResult(carLocations));

            var carLocationService = new CarLocationService(_mockCarLocationRepository.Object);


            // Act
            var response = await carLocationService.SearchCarLocationsAsync(userLongitude, userLatitude);


            // Assert
            Assert.NotNull(response.Message);
        }

        [Fact]
        public async Task UpdateCarLocationsAsync_ShouldBe_Success()
        {
            // Arrange
            List<CarLocation> carLocations = new List<CarLocation>();
            for (int i = 1; i <= 5; i++)
            {
                carLocations.Add(new CarLocation
                {
                    CarDriverId = i,
                    Longitude = new Random().Next(-5, 5),
                    Latitude = new Random().Next(-5, 5)
                });
            }

            _mockCarLocationRepository.Setup(a => a.ListAsync()).Returns(Task.FromResult(carLocations));

            var carLocationService = new CarLocationService(_mockCarLocationRepository.Object);


            // Act
            var response = await carLocationService.UpdateCarLocationsAsync();


            // Assert
            Assert.True((bool)response.Data);
        }
    }
}
