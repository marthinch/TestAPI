using TestAPI.Models;
using TestAPI.Repositories;
using TestAPI.Responses;

namespace TestAPI.Services
{
    public interface ICarLocationService
    {
        Task<BaseResponse> GetCarLocationsAsync();
        Task<BaseResponse> SearchCarLocationsAsync(double userLongitude, double userLatitude);
        Task<BaseResponse> UpdateCarLocationsAsync();
    }

    public class CarLocationService : ICarLocationService
    {
        private readonly IBaseRepository<CarLocation> _carLocationRepository;

        public async Task<BaseResponse> GetCarLocationsAsync()
        {
            var carLocations = await _carLocationRepository.ListAsync();
            return new BaseResponse(carLocations);
        }

        public CarLocationService(IBaseRepository<CarLocation> carLocationRepository)
        {
            _carLocationRepository = carLocationRepository;
        }

        public async Task<BaseResponse> SearchCarLocationsAsync(double userLongitude, double userLatitude)
        {
            List<CarLocation> nearestCarLocations = new List<CarLocation>();

            var carLocations = await _carLocationRepository.ListAsync();
            if (carLocations == null || !carLocations.Any())
            {
                return new BaseResponse(null, "No nearest car available");
            }

            foreach (var carLocation in carLocations)
            {
                var carLongitude = carLocation.Longitude;
                var carLatitude = carLocation.Latitude;

                var delta = (carLongitude - userLongitude) + (carLatitude - userLatitude);
                if (delta > 0 && delta <= 2)
                {
                    nearestCarLocations.Add(carLocation);
                }
            }

            if (!nearestCarLocations.Any())
            {
                return new BaseResponse(null, "No nearest car available");
            }

            if (nearestCarLocations.Count > 2)
            {
                return new BaseResponse(null, "Unable to book car");
            }

            return new BaseResponse(nearestCarLocations);
        }

        public async Task<BaseResponse> UpdateCarLocationsAsync()
        {
            bool isUpdated = false;

            var carLocations = await _carLocationRepository.ListAsync();
            foreach (var carLocation in carLocations)
            {
                carLocation.Longitude = new Random().Next(-10, 10);
                carLocation.Latitude = new Random().Next(-10, 10);

                await _carLocationRepository.UpdateAsync(carLocation.Id, carLocation);
            }

            isUpdated = true;

            return new BaseResponse(isUpdated);
        }
    }
}
