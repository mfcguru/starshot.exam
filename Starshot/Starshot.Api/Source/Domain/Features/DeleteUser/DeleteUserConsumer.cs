using MassTransit;
using MediatR;

namespace Starshot.Api.Source.Domain.Features.DeleteUser
{
    public class DeleteUserConsumer : IConsumer<DeleteUserParameter>
    {
        private readonly IMediator mediator;
        private readonly ILogger<DeleteUserConsumer> logger;

        public DeleteUserConsumer(IMediator mediator, ILogger<DeleteUserConsumer> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<DeleteUserParameter> context)
        {
            logger.LogDebug("delete user event was triggered");

            await mediator.Send(new DeleteUserCommand(context.Message.UserId));
        }
    }
}
