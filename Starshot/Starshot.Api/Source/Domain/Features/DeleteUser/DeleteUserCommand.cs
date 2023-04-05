using MediatR;
using Starshot.Api.Source.Domain.BusinessRules;
using Starshot.Api.Source.Domain.Entities;

namespace Starshot.Api.Source.Domain.Features.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        private readonly int userId;
        public DeleteUserCommand(int userId) => this.userId = userId;
        public DeleteUserCommand(DeleteUserParameter parameter) => this.userId = parameter.UserId;

        public class Handler : IRequestHandler<DeleteUserCommand>
        {
            private readonly DataContext context;
            public Handler(DataContext context) => this.context = context;

            public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await context.Users.FindAsync(request.userId);
                if (user == null)
                {
                    throw new ResourceNotFoundException();
                }

                user.Active = false;

                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
