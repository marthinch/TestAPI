using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleDataController : ControllerBase
    {
        private readonly TestAPIContext _testAPIContext;

        public SampleDataController(TestAPIContext testAPIContext)
        {
            _testAPIContext = testAPIContext;
        }

        [HttpGet]
        [Route("Generate")]
        public async Task<IActionResult> GenerateSampleDataAsync()
        {
            int totalData = 10;

            for (int i = 1; i <= totalData; i++)
            {
                _testAPIContext.Car.Add(new Models.Car
                {
                    NumberPlate = new Random().Next(1000, 9999).ToString(),
                    Color = "Black",
                    Type = "Luxury"
                });

                _testAPIContext.CarLocation.Add(new Models.CarLocation
                {
                    CarDriverId = i,
                    Longitude = new Random().Next(-10, 10),
                    Latitude = new Random().Next(-10, 10),
                });
            }

            var isSaved = await _testAPIContext.SaveChangesAsync();
            return Ok(isSaved > 0);
        }

        [HttpGet]
        [Route("Degenerate")]
        public IActionResult DegenerateSampleData()
        {
            _testAPIContext.Database.ExecuteSqlRaw("TRUNCATE TABLE Car");
            _testAPIContext.Database.ExecuteSqlRaw("TRUNCATE TABLE CarLocation");

            return Ok(true);
        }
    }
}
