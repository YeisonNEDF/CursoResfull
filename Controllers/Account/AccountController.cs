using Core.DTOs.User;
using Core.Features.Authenticate.Commands.AuthenticateCommand;
using Core.Features.Authenticate.Commands.RegisterCommand;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequestDto requestDto)
        {
            return Ok(await Mediator.Send(new AuthenticateCommand
            {
                Email = requestDto.Email,
                Password = requestDto.Password,
                IpAddress = GenerateIpAddress()
            }));
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequestDto requestDto)
        {

            return Ok(await Mediator.Send(new RegisterCommand
            {
                Nombre = requestDto.Nombre,
                Apellido = requestDto.Apellido,
                Email = requestDto.Email,
                Password = requestDto.Password,
                ConfirmPassword = requestDto.ConfirmPassword,
                UserName = requestDto.UserName,
                Origin = Request.Headers["origin"]
               
            }));
        }
        private string GenerateIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))            
                return Request.Headers["X-Forwarded-For"];
            
            else            
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            
        }
    }
}
