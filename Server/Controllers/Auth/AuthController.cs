using BeastBattle.Server.Services.Contracts;
using BeastBattle.Shared.Dtos;
using BeastBattle.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeastBattle.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(RegisterDto request)
        {
            var response = await this.authService.Register(
                new User
                {
                    Username = request.Username,
                    Name = request.Name,
                    Surname = request.Surname,
                    Email = request.Email
                },
                request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDto request)
        {
            var response = await this.authService.Login(request.Username, request.Password);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }

}