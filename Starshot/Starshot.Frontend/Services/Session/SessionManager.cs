using Microsoft.AspNetCore.Mvc;

namespace Starshot.Frontend.Services.Session
{
    public class SessionManager : ISessionManager
    {
        private const string SESSION_TOKEN = "SESSION_TOKEN";

        public string? GetToken(Controller controllerInstance)
        {
            return controllerInstance.Request.Cookies[SESSION_TOKEN];
        }

        public void SetToken(Controller controllerInstance, string token)
        {
            controllerInstance.Response.Cookies.Append(SESSION_TOKEN, token);
        }

        public void ClearToken(Controller controllerInstance)
        {
            controllerInstance.Response.Cookies.Delete(SESSION_TOKEN);
        }
    }
}
