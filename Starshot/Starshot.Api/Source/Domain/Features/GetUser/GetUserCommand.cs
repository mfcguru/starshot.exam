using MediatR;
using Starshot.Api.Source.Domain.BusinessRules;
using Starshot.Api.Source.Domain.Entities;

namespace Starshot.Api.Source.Domain.Features.GetUser
{
    public class GetUserCommand : IRequest<GetUserResult>
    {
        private readonly int userId;
        public GetUserCommand(int userId) => this.userId = userId;

        public class Handler : IRequestHandler<GetUserCommand, GetUserResult>
        {
            private readonly DataContext context;
            public Handler(DataContext context) => this.context = context;

            public async Task<GetUserResult> Handle(GetUserCommand request, CancellationToken cancellationToken)
            {
                var user = await context.Users.FindAsync(request.userId, cancellationToken);
                if (user == null)
                {
                    throw new ResourceNotFoundException();
                }

                return new GetUserResult
                {
                    UserId = user.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    TimeIn = user.TimeIn,
                    TimeOut = user.TimeOut,
                    Active = user.Active
                };
            }
        }
    }
}
