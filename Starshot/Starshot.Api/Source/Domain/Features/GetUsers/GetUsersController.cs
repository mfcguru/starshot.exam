using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Starshot.Api.Source.Domain.Enums;

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
        public async Task<IActionResult> GetUsers([FromQuery]string? filterName = "", [FromQuery]FilterActive filterActive = FilterActive.None)
        {
            var result = await mediator.Send(new GetUsersCommand(filterName, filterActive));

            return Ok(result);
        }
    }
}
