using BookStore_BL.Interfaces;
using BookStore_BL.Services;
using BookStore_Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
    

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet( "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _authorService.GetAll();
            if (result == null && result.Count == 0) return NotFound();  
            return Ok(result);  
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id) 
        {
            if (id < 0) return BadRequest(id);
            var result = await _authorService.GetById(id);
            return result != null ? Ok(result) : NotFound(id);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] Author author)
        {
            if (author == null) return BadRequest(author);
            await _authorService.Add(author);

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0) return BadRequest(id);
            await _authorService.Delete(id);
            return Ok();
        }
    }
}
