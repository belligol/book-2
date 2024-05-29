using BookStore_BL.Interfaces;
using BookStore_BL.Services;
using BookStore_Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookProducerController : ControllerBase
    {
       private readonly IProduceBookService _produceBookService;
       

        public BookProducerController(IProduceBookService produceBookService)
        {
            _produceBookService = produceBookService;
   
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Book book)
        {
            if (book == null) return BadRequest(book);
            await _produceBookService.ProduceBook(book);
            
            return Ok();

        }
    }
}
