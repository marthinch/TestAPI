using GeoCoordinatePortable;
using TestAPI.Models;
using TestAPI.Repositories;
using TestAPI.Responses;

namespace TestAPI.Services
{
    public interface IBookService
    {
        Task<BaseResponse> BookCarAsync(BookRequest request);
    }

    public class BookService : IBookService
    {
        private readonly IBaseRepository<Book> _bookRepository;

        public BookService(IBaseRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BaseResponse> BookCarAsync(BookRequest request)
        {
            if (request != null)
            {
                if (request.FromLatitude == 0 || request.FromLongitude == 0)
                {
                    return new BaseResponse("Your origin location not valid");
                }

                if (request.ToLatitude == 0 || request.ToLongitude == 0)
                {
                    return new BaseResponse("Your destination location not valid");
                }
            }

            var fromCoordinate = new GeoCoordinate(request.FromLatitude, request.FromLongitude);
            var toCoordnate = new GeoCoordinate(request.ToLongitude, request.ToLatitude);
            var distance = Math.Round((fromCoordinate.GetDistanceTo(toCoordnate) / 1000), 2); // in KM

            var isSaved = await _bookRepository.SaveAsync(new Book
            {
                UserId = request.UserId,
                CarDriverId = request.CarDriverId,
                From = request.From,
                FromLatitude = request.FromLatitude,
                FromLongitude = request.FromLongitude,
                To = request.To,
                ToLatitude = request.ToLatitude,
                ToLongitude = request.ToLongitude,
                Distance = distance
            });
            return new BaseResponse(isSaved);
        }
    }
}
