using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Starshot.Api.Source.Domain.Features.EditUser
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EditUserController : ControllerBase
    {
        private readonly IMediator mediator;
        public EditUserController(IMediator mediator) => this.mediator = mediator;

        [HttpPut("{userId}")]
        public async Task<IActionResult> AddUser(int userId, EditUserParameters parameters)
        {
            await mediator.Send(new EditUserCommand(userId, parameters));

            return Ok();
        }
    }
}
