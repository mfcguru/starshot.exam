using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NuGet.Common;
using Starshot.Frontend.Filters;
using Starshot.Frontend.Models;
using Starshot.Frontend.Services.Api;
using Starshot.Frontend.Services.Session;

namespace Starshot.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiService apiService;
        private readonly ISessionManager sessionManager;
        private readonly IOptions<AppSettings> appSettings;

        public HomeController(
            IApiService apiService,
            ISessionManager sessionManager,
            IOptions<AppSettings> appSettings)
        {
            this.apiService = apiService;
            this.sessionManager = sessionManager;   
            this.appSettings = appSettings; 
        }

        [HttpGet]
        [SessionStateFilter]
        public async Task<IActionResult> Index()
        {
            var result = await apiService.GetUsers(sessionManager.GetToken(this));
            if (result.Success)
            {
                var users = JsonConvert.DeserializeObject<List<UserListViewModel>>(result.JsonData);
                return View(users);
            }

            ViewBag.Error = result.ErrorMessage;
            return View();
        }

        [HttpGet]
        [SessionStateFilter]
        public IActionResult AddUser(SessionModel session)
        {
            ViewBag.Session = session;
            return View(new AddUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(string session, AddUserViewModel model)
        {
            var result = await apiService.AddUser(session, model.FirstName, model.LastName, model.TimeIn, model.TimeOut);
            if (result.Success)
            {
                return RedirectToAction("Index", new { session });
            }

            ViewBag.Session = session;
            ViewBag.ErrorMessage = result.ErrorMessage;
            return View(new AddUserViewModel());
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> EditUser(string session, int userId)
        {
            var result = await apiService.GetUser(session, userId);
            if (result.Success)
            {
                var vm = JsonConvert.DeserializeObject<EditUserViewModel>(result.JsonData);
                ViewBag.Session = session;
                return View(vm);    
            }

            ViewBag.Error = result.ErrorMessage;
            return RedirectToAction("Index", new { session });
        }

        private string SessionToken { get { return (string)TempData["session_token"]; } }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel
            {
                Username = appSettings.Value.HardcodedUsername,
                Password = appSettings.Value.HardcodedPasswword
            };

            ViewBag.LoginView = true;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await apiService.Login(model.Username, model.Password);
            if (result.Success)
            {
                var session = JsonConvert.DeserializeObject<SessionModel>(result.JsonData);

                sessionManager.SetToken(this, session.Token); 

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = result.ErrorMessage;
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            sessionManager.ClearToken(this);

            return RedirectToAction("Index");
        }
    }
}