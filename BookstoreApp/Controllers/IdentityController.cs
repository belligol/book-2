using BookStore_BL.Interfaces;
using BookStore_Models.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApp.Controllers
{
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;   
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest();
            }

            var authResult = await _identityService.RegisterAsync(request.UserName, request.Password, request.Email);    

            if (!authResult.isSuccessfull)
            {
                return Problem($"Error register user");
            }

            return Ok(authResult);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRegistrationRequest request)
        {

            if (request == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest();
            }

            var loginResponse = await _identityService.LoginAsync(request.UserName, request.Password);
            if (!loginResponse.isSuccessfull) {
                return Problem($"Error login user!");
                   }
            return Ok(loginResponse);
        }

        [HttpPost("logoff")]
        public async Task<IActionResult> LogOff()
        {
            await _identityService.LogOff();
            return Ok();
        }
    }
}
