using Microsoft.AspNetCore.Mvc;
using TestAPI.Models;
using TestAPI.Responses;
using TestAPI.Services;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookingsController(IBookService bookService)
        {
            _bookService = bookService;
        }
       
        [HttpPost]
        public Task<BaseResponse> BookCarAsync(BookRequest request)
        {
            return _bookService.BookCarAsync(request);
        }
    }
}
