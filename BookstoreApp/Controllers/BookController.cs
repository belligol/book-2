using BookStore_BL.Interfaces;
using BookStore_BL.Services;
using BookStore_Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("Get All")]
        public async Task<IActionResult> GetAll()
        {
           var result = await _bookService.GetAll();
            if (result != null && result.Count > 0) return NoContent();
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Book book)
        {
            if (book == null) return BadRequest(book);
            await _bookService.Add(book);

            return Ok();
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _bookService.GetById(id);
            return result != null ? Ok(result) : NotFound(id);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteById(Guid id) 
        {
            await _bookService.Delete(id);
            return Ok();
        }

    }
}