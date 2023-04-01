using MediatR;
using Microsoft.EntityFrameworkCore;
using Starshot.Api.Source.Domain.Entities;
using Starshot.Api.Source.Domain.Enums;

namespace Starshot.Api.Source.Domain.Features.GetUsers
{
    public class GetUsersCommand : IRequest<List<GetUsersResult>>
    {
        private readonly string? filterName;
        private readonly FilterActive filterActive;
        public GetUsersCommand(string? filterName, FilterActive filterActive)
        {
            this.filterName = filterName;
            this.filterActive = filterActive;
        }

        public class Handler : IRequestHandler<GetUsersCommand, List<GetUsersResult>>
        {
            private readonly DataContext context;            
            public Handler(DataContext context) => this.context = context;

            public async Task<List<GetUsersResult>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
            {
                var query = context.Users.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(request.filterName))
                {
                    query = query.Where(o => o.FirstName.ToLower().Contains(request.filterName.ToLower()) 
                        || o.LastName.ToLower().Contains(request.filterName.ToLower()));
                }

                switch (request.filterActive)
                {
                    case FilterActive.True:
                        query = query.Where(o => o.Active);
                        break;
                    case FilterActive.False:
                        query = query.Where(o => !o.Active);
                        break;
                }

                var result = (await query.ToListAsync(cancellationToken))
                    .Select(o => new GetUsersResult {
                        UserId = o.UserID,
                        FullName = $"{o.LastName.ToUpper()}, {o.FirstName}",
                        TimeIn = o.TimeIn,
                        TimeOut = o.TimeOut,
                        Active = o.Active})
                    .OrderBy(o => o.FullName)
                    .ToList();

                return result;
            }
        }
    }
}
