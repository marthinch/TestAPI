using Microsoft.AspNetCore.Mvc;
using TestAPI.Responses;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public Task<BaseResponse> GetCar()
        {
            return _carService.GetCarsAsync();
        }

        [HttpGet("{id}")]
        public Task<BaseResponse> GetCar(int id)
        {
            return _carService.GetCarAsync(id);
        }
    }
}
