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
    public class CarServiceUnitTest
    {
        private readonly Mock<IBaseRepository<Car>> _mockCarRepository;

        private CarService _carService;

        public CarServiceUnitTest()
        {
            _mockCarRepository = new Mock<IBaseRepository<Car>>();
        }

        [Fact]
        public async Task GetCarsAsync_ShouldBe_Available()
        {
            // Arrange
            List<Car> cars = new List<Car>();
            for (int i = 1; i <= 5; i++)
            {
                cars.Add(new Car
                {
                    Id = i,
                    NumberPlate = new Random().Next(1000, 9999).ToString(),
                    Color = "Black",
                    Type = "Luxury"
                });
            }

            _mockCarRepository.Setup(a => a.ListAsync()).Returns(Task.FromResult(cars));

            _carService = new CarService(_mockCarRepository.Object);

            int expectedCarLocationCount = cars.Count;


            // Act
            var response = await _carService.GetCarsAsync();
            var carCount = ((List<Car>)response.Data).Count;


            // Assert
            Assert.NotNull(response.Data);
            Assert.Equal(expectedCarLocationCount, carCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public async Task GetCarAsync_ShouldBe_Available(int id)
        {
            // Arrange
            var car = new Car
            {
                Id = id,
                NumberPlate = new Random(4).Next().ToString(),
                Color = "Black",
                Type = "Luxury"
            };

            _mockCarRepository.Setup(a => a.DetailAsync(id)).Returns(ValueTask.FromResult(car));

            _carService = new CarService(_mockCarRepository.Object);


            // Act
            var response = await _carService.GetCarAsync(id);


            // Assert
            Assert.NotNull(response.Data);
        }
    }
}
