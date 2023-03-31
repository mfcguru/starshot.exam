using System.Globalization;
using System.Net;

namespace Starshot.Api.Source.Domain.BusinessRules
{
    public abstract class BusinessRuleException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        protected BusinessRuleException(HttpStatusCode statusCode) : base() { StatusCode = statusCode; }

        protected BusinessRuleException(HttpStatusCode statusCode, string message) : base(message) { StatusCode = statusCode; }

        protected BusinessRuleException(HttpStatusCode statusCode, string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
            StatusCode = statusCode;
        }
    }
}
