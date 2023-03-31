using System.Net;

namespace Starshot.Api.Source.Domain.BusinessRules
{
    public class ResourceNotFoundException : BusinessRuleException
    {
        private const string message = "Resource was not found";

        public ResourceNotFoundException() : base(HttpStatusCode.NotFound, message) { }
    }
}
