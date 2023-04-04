using MassTransit;
using MediatR;

namespace Starshot.Api.Source.Domain.Features.AddUser
{
    public class AddUserConsumer : IConsumer<AddUserParameters>
    {
        private readonly IMediator mediator;
        private readonly ILogger<AddUserConsumer> logger;

        public AddUserConsumer(IMediator mediator, ILogger<AddUserConsumer> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<AddUserParameters> context)
        {
            logger.LogDebug("add user event was triggered");

            await mediator.Send(new AddUserCommand(context.Message));
        }
    }
}
