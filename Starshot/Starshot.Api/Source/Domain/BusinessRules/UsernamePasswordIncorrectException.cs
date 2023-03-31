using System.Net;

namespace Starshot.Api.Source.Domain.BusinessRules
{
    public class UsernamePasswordIncorrectException : BusinessRuleException
    {
        private const string message = "Username or password is incorrect";

        public UsernamePasswordIncorrectException() : base(HttpStatusCode.Forbidden, message) { }
    }
}
