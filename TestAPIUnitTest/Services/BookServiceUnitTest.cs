using Moq;
using System.Threading.Tasks;
using TestAPI.Models;
using TestAPI.Repositories;
using TestAPI.Services;
using Xunit;

namespace TestAPIUnitTest.Services
{
    public class BookServiceUnitTest
    {
        private readonly Mock<IBaseRepository<Book>> _mockBookRepository;

        private BookService _bookService;

        public BookServiceUnitTest()
        {
            _mockBookRepository = new Mock<IBaseRepository<Book>>();
        }

        [Theory]
        [InlineData(0, 0, 1.265362, 103.8215518)]
        public async Task BookCarAsync_ShouldBe_Fail_DueToOriginLocation(double fromLatitude, double fromLongitude, double toLatitude, double toLongitude)
        {
            // Arrange
            _mockBookRepository.Setup(x => x.SaveAsync(It.IsAny<Book>())).Returns(Task.FromResult(true));
            _bookService = new BookService(_mockBookRepository.Object);


            // Act
            BookRequest bookRequest = new BookRequest
            {
                UserId = 1,
                CarDriverId = 1,
                From = "Home",
                FromLatitude = fromLatitude,
                FromLongitude = fromLongitude,
                To = "Loc",
                ToLatitude = toLongitude,
                ToLongitude = toLatitude
            };
            var bookResponse = await _bookService.BookCarAsync(bookRequest);


            // Assert
            Assert.NotNull(bookResponse.Message);
        }

        [Theory]
        [InlineData(1.283375, 103.860725, 0, 0)]
        public async Task BookCarAsync_ShouldBe_Fail_DueToDestinationLocation(double fromLatitude, double fromLongitude, double toLatitude, double toLongitude)
        {
            // Arrange
            _mockBookRepository.Setup(x => x.SaveAsync(It.IsAny<Book>())).Returns(Task.FromResult(true));
            _bookService = new BookService(_mockBookRepository.Object);


            // Act
            BookRequest bookRequest = new BookRequest
            {
                UserId = 1,
                CarDriverId = 1,
                From = "Home",
                FromLatitude = fromLatitude,
                FromLongitude = fromLongitude,
                To = "Loc",
                ToLatitude = toLongitude,
                ToLongitude = toLatitude
            };
            var bookResponse = await _bookService.BookCarAsync(bookRequest);


            // Assert
            Assert.NotNull(bookResponse.Message);
        }

        [Theory]
        [InlineData(1.283375, 103.860725, 1.265362, 103.8215518)] // from MBS to HFT
        public async Task BookCarAsync_ShouldBe_Success(double fromLatitude, double fromLongitude, double toLatitude, double toLongitude)
        {
            // Arrange
            _mockBookRepository.Setup(x => x.SaveAsync(It.IsAny<Book>())).Returns(Task.FromResult(true));
            _bookService = new BookService(_mockBookRepository.Object);


            // Act
            BookRequest bookRequest = new BookRequest
            {
                UserId = 1,
                CarDriverId = 1,
                From = "Home",
                FromLatitude = fromLatitude,
                FromLongitude = fromLongitude,
                To = "Loc",
                ToLatitude = toLongitude,
                ToLongitude = toLatitude
            };
            var bookResponse = await _bookService.BookCarAsync(bookRequest);


            // Assert
            Assert.True((bool)bookResponse.Data);
        }
    }
}
