using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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

        public HomeController(IApiService apiService, ISessionManager sessionManager, IOptions<AppSettings> appSettings)
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
        public IActionResult AddUser()
        {
            return View(new AddUserViewModel());
        }

        [HttpPost]
        [SessionStateFilter]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            var result = await apiService.AddUser(sessionManager.GetToken(this), model.FirstName, model.LastName, model.TimeIn, model.TimeOut);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }

            ViewBag.ErrorMessage = result.ErrorMessage;
            return View(new AddUserViewModel());
        }

        [HttpGet("EditUser/{userId}")]
        [SessionStateFilter]
        public async Task<IActionResult> EditUser(int userId)
        {
            var result = await apiService.GetUser(sessionManager.GetToken(this), userId);
            if (result.Success)
            {
                var model = JsonConvert.DeserializeObject<EditUserViewModel>(result.JsonData);
                return View(model);    
            }

            ViewBag.Error = result.ErrorMessage;
            return RedirectToAction("Index");
        }

        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var result = await apiService.EditUser(sessionManager.GetToken(this), model.UserId, model.FirstName, model.LastName, model.TimeIn, model.TimeOut, model.Active);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Error = result.ErrorMessage;
            return View(model);
        }

        [HttpGet("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var result = await apiService.DeleteUser(sessionManager.GetToken(this), userId);
            if (result.Success)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Error = result.ErrorMessage;
            return RedirectToAction("Index");
        }

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