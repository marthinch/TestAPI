using Microsoft.AspNetCore.Mvc;
using TestAPI.Responses;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarLocationsController : ControllerBase
    {
        private readonly ICarLocationService _carLocationService;

        public CarLocationsController(ICarLocationService carLocationService)
        {
            _carLocationService = carLocationService;
        }

        [HttpGet]
        public Task<BaseResponse> GetCarLocationsAsync()
        {
            return _carLocationService.GetCarLocationsAsync();
        }

        [HttpGet]
        [Route("Search")]
        public Task<BaseResponse> SearchCarLocationsAsync(double userLongitude, double userLatitude)
        {
            return _carLocationService.SearchCarLocationsAsync(userLongitude, userLatitude);
        }

        [HttpGet]
        [Route("Update")]
        public Task<BaseResponse> UpdateCarLocationsAsync()
        {
            return _carLocationService.UpdateCarLocationsAsync();
        }
    }
}
