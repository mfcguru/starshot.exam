using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Starshot.Api.Source.Domain.Features.GetUsers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GetUsersController : ControllerBase
    {
        private readonly IMediator mediator;
        public GetUsersController(IMediator mediator) => this.mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await mediator.Send(new GetUsersCommand());

            return Ok(result);
        }
    }
}
