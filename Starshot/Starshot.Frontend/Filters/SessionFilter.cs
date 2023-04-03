using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Starshot.Frontend.Services.Session;

namespace Starshot.Frontend.Filters
{
    public class SessionFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerInstance = (Controller)context.Controller;
            var sessionManager = (ISessionManager?)context.HttpContext.RequestServices.GetService(typeof(ISessionManager));

            if (sessionManager.GetToken(controllerInstance) == null)
            {
                context.Result = controllerInstance.RedirectToAction("login", "home");
            }
        }
    }
}
