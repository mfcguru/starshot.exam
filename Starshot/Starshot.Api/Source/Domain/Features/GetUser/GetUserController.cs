using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Starshot.Api.Source.Domain.Features.GetUser
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GetUserController : ControllerBase
    {
        private readonly IMediator mediator;
        public GetUserController(IMediator mediator) => this.mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetUser(int userId)
        {
            var result = await mediator.Send(new GetUserCommand(userId));

            return Ok(result);
        }
    }
}
