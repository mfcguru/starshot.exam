using MediatR;
using Starshot.Api.Source.Domain.BusinessRules;
using Starshot.Api.Source.Domain.Entities;

namespace Starshot.Api.Source.Domain.Features.EditUser
{
    public class EditUserCommand : IRequest
    {
        private readonly int userId;
        private readonly EditUserParameters parameters;
        public EditUserCommand(int userId, EditUserParameters parameters)
        {
            this.userId = userId;
            this.parameters = parameters;
        }

        public class Handler : IRequestHandler<EditUserCommand>
        {
            private readonly DataContext context;
            public Handler(DataContext context) => this.context = context;

            public async Task Handle(EditUserCommand request, CancellationToken cancellationToken)
            {
                var user = await context.Users.FindAsync(request.userId, cancellationToken);
                if (user == null)
                {
                    throw new ResourceNotFoundException();
                }

                user.FirstName = request.parameters.FirstName;
                user.LastName = request.parameters.LastName;
                user.TimeIn = request.parameters.TimeIn;
                user.TimeOut = request.parameters.TimeOut;
                user.Active = request.parameters.Active;

                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
