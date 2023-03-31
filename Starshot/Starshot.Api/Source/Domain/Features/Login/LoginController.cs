using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Starshot.Api.Source.Domain.Features.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator mediator;
        public LoginController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginParameters parameters)
        {
            string token = await mediator.Send(new LoginCommand(parameters));

            return Ok(token);
        }
    }
}
