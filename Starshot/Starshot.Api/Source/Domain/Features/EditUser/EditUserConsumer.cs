using MassTransit;
using MediatR;

namespace Starshot.Api.Source.Domain.Features.EditUser
{
    public class EditUserConsumer : IConsumer<EditUserParameters>
    {
        private readonly IMediator mediator;
        private readonly ILogger<EditUserConsumer> logger;

        public EditUserConsumer(IMediator mediator, ILogger<EditUserConsumer> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<EditUserParameters> context)
        {
            logger.LogDebug("edit user event was triggered");

            await mediator.Send(new EditUserCommand(context.Message.UserId, context.Message));
        }
    }
}
