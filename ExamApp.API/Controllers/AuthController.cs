using ExamApp.Application.Features.Auth.Commands.LoginUser;
using ExamApp.Application.Features.Auth.Commands.Logout;
using ExamApp.Application.Features.Auth.Commands.RefreshToken;
using ExamApp.Application.Features.Auth.Commands.RegisterUser;

namespace ExamApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }



        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }



        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var result = await mediator.Send(new LogoutCommand());
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return NoContent();
        }
    }
}
