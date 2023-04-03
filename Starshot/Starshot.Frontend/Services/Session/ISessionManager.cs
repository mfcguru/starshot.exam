using Microsoft.AspNetCore.Mvc;

namespace Starshot.Frontend.Services.Session
{
    public interface ISessionManager
    {
        string? GetToken(Controller controllerInstance);
        void SetToken(Controller controllerInstance, string token);
        void ClearToken(Controller controllerInstance);
    }
}
