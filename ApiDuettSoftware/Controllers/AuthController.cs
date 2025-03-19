using ApiDuettSoftware.Dto;
using ApiDuettSoftware.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiDuettSoftware.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public IActionResult Auth([FromBody] AuthDto data)
        {
            var token = _authService.Authenticate(data.Email, data.Password);

            if (token is null)
            {
                return BadRequest("Usuário ou senha inválido");
            }

            return Ok(new { token });
        }

    }
}
