using TestAPI.Models;
using TestAPI.Repositories;
using TestAPI.Responses;

namespace TestAPI.Services
{
    public interface ICarService
    {
        Task<BaseResponse> GetCarsAsync();
        Task<BaseResponse> GetCarAsync(int id);
    }

    public class CarService : ICarService
    {
        private readonly IBaseRepository<Car> _carRepository;

        public CarService(IBaseRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<BaseResponse> GetCarsAsync()
        {
            var cars = await _carRepository.ListAsync();
            if (!cars.Any())
            {
                return new BaseResponse(null, "No cars available");
            }

            return new BaseResponse(cars);
        }

        public async Task<BaseResponse> GetCarAsync(int id)
        {
            var item = await _carRepository.DetailAsync(id);
            return new BaseResponse(item);
        }
    }
}
