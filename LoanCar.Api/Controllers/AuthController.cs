using LoanCar.Api.Services;
using LoanCar.Shared.Requests;
using LoanCar.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LoanCar.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController(AuthService authService, TokenService tokenService) : ControllerBase
    {
        private readonly AuthService authService = authService;
        private readonly TokenService tokenService = tokenService;

        [HttpPut("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var user = authService.Login(loginDTO.Email, loginDTO.Password);
                var token = tokenService.NewToken(user);
                return Ok(new AuthTokenDTO()
                {
                    Token = token
                });
            }
            catch
            {
                return Unauthorized("Invalid username or password");
            }
        }
    }
}
