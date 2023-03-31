using MediatR;
using Microsoft.EntityFrameworkCore;
using Starshot.Api.Source.Domain.Entities;

namespace Starshot.Api.Source.Domain.Features.GetUsers
{
    public class GetUsersCommand : IRequest<List<GetUsersResult>>
    {
        public class Handler : IRequestHandler<GetUsersCommand, List<GetUsersResult>>
        {
            private readonly DataContext context;
            public Handler(DataContext context) => this.context = context;

            public async Task<List<GetUsersResult>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
            {
                var users = await context.Users
                    .AsNoTracking()
                    .Select(o => new GetUsersResult
                    {
                        UserId = o.UserID,
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        TimeIn = o.TimeIn,
                        TimeOut = o.TimeOut,
                        Active = o.Active,
                        FullName = $"{o.LastName.ToUpper()}, {o.FirstName}"
                    }).ToListAsync(cancellationToken);

                return users;
            }
        }
    }
}
