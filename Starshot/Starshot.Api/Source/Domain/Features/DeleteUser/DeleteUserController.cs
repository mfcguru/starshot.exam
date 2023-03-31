using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Starshot.Api.Source.Domain.Features.EditUser
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteUserController : ControllerBase
    {
        private readonly IMediator mediator;
        public DeleteUserController(IMediator mediator) => this.mediator = mediator;

        [HttpDelete("{userId}")]
        public async Task<IActionResult> EditUser(int userId)
        {
            await mediator.Send(new DeleteUserCommand(userId));

            return Ok();
        }
    }
}
