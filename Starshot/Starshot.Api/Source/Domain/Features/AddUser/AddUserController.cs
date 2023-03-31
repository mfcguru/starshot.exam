using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Starshot.Api.Source.Domain.Features.AddUser
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddUserController : ControllerBase
    {
        private readonly IMediator mediator;
        public AddUserController(IMediator mediator) => this.mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserParameters parameters)
        {
            await mediator.Send(new AddUserCommand(parameters));

            return Ok();
        }
    }
}
