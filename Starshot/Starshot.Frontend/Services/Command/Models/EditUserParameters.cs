
namespace Starshot.Frontend.Services.Command.Models
{
    public record EditUserParameters : AddUserParameters
    {
        public int UserId { get; init; } 
        public bool Active { get; init; }
    }
}
