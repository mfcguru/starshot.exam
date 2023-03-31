using Starshot.Api.Source.Domain.BaseModels;

namespace Starshot.Api.Source.Domain.Features.GetUser
{
    public class GetUserResult : UserModel
    {
        public int UserId { get; set; }
        public bool Active { get; set; }
    }
}
