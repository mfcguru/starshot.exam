using MediatR;
using Starshot.Api.Source.Domain.Entities;

namespace Starshot.Api.Source.Domain.Features.AddUser
{
    public class AddUserCommand : IRequest
    {
        private readonly AddUserParameters parameters;
        public AddUserCommand(AddUserParameters parameters) => this.parameters = parameters;  
        
        public class Handler : IRequestHandler<AddUserCommand>
        {
            private readonly DataContext context;
            public Handler(DataContext context) => this.context = context;

            public async Task<Unit> Handle(AddUserCommand request, CancellationToken cancellationToken)
            {
                context.Users.Add(new User
                {
                    FirstName = request.parameters.FirstName,
                    LastName = request.parameters.LastName,
                    TimeIn = request.parameters.TimeIn,
                    TimeOut = request.parameters.TimeOut
                });

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
