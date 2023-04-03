
namespace Starshot.Api.Source.Domain.Features.Login
{
    public class LoginResult
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
