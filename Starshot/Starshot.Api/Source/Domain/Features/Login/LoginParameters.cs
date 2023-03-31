using System.ComponentModel.DataAnnotations;

namespace Starshot.Api.Source.Domain.Features.Login
{
    public class LoginParameters
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
