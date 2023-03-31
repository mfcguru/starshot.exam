using Starshot.Api.Source.Domain.BaseModels;

namespace Starshot.Api.Source.Domain.Features.GetUsers
{
    public class GetUsersResult : UserModel
    {
        public int UserId { get; set; }
        public bool Active { get; set; }
        public string FullName { get; set; }    
    }
}
