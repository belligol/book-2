using BookStore_BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LibraryController : ControllerBase
    {
        public readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }


        [HttpPost("GetAllBooksByAuthorId")]
        public async Task<GetAllBookByAuthorResponse> GetAllBookByAuthorAfterDate(GetAllBookByAuthorRequest request)
        {
            return await _libraryService.GetAllBookByAuthorAfterDate(request);
        }

        [HttpPost("TestEndPoint")]
        public string TestEndPoint([FromBody]

            GetAllBookByAuthorRequest request)
        {
            return "Ok";
        }
    }

 
}