using Starshot.Api.Source.Domain.BaseModels;

namespace Starshot.Api.Source.Domain.Features.EditUser
{
    public class EditUserParameters : UserModel
    {
        public int UserId { get; set; }
        public bool Active { get; set; }
    }
}
