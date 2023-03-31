using Starshot.Api.Source.Domain.BaseModels;

namespace Starshot.Api.Source.Domain.Features.EditUser
{
    public class EditUserParameters : UserModel
    {
        public bool Active { get; set; }
    }
}
