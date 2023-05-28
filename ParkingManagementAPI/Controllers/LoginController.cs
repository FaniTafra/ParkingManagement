using DomainModel.DtoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingManagementAPI.Repository;
using ParkingManagementAPI.Services;

namespace ParkingManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = _loginService.Authenticate(userLogin);
            if (user != null)
            {
                var token = _loginService.Generate(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }
    }
}
