using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Starshot.Api.Source.Domain.BusinessRules;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Starshot.Api.Source.Domain.Features.Login
{
    public class LoginCommand : IRequest<string>
    {
        private readonly LoginParameters parameters;
        public LoginCommand(LoginParameters parameters) => this.parameters = parameters;

        public class Handler : IRequestHandler<LoginCommand, string>
        {
            private readonly IOptions<AppSettings> appSettings;
            public Handler(IOptions<AppSettings> appSettings) => this.appSettings = appSettings;

            public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run(() =>
                {
                    if (request.parameters.Username != appSettings.Value.HardcodedUsername)
                    {
                        throw new UsernamePasswordIncorrectException();
                    }

                    if (request.parameters.Password != appSettings.Value.HardcodedPasswword)
                    {
                        throw new UsernamePasswordIncorrectException();
                    }

                    return GenerateJwtToken();
                });
            }

            private string GenerateJwtToken()
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.Value.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "Default Admin")
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return tokenString;
            }
        }
    }
}
