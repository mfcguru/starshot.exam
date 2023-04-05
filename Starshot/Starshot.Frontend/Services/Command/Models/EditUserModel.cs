
namespace Starshot.Frontend.Services.Command.Models
{
    public record EditUserModel : AddUserModel
    {
        public int UserId { get; init; } 
        public bool Active { get; init; }
    }
}
