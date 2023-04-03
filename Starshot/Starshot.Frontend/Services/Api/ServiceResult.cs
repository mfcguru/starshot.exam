using System.Net;

namespace Starshot.Frontend.Services.Api
{
    public class ServiceResult
    {
        public HttpStatusCode? StatusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string JsonData { get; set; } = string.Empty;
        public bool Success { get; set; } = true;
    }
}
