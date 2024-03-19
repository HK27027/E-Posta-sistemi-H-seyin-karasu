using EMailSendingApplication.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace EMailSendingApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Route("Register")]
        public IActionResult Register()
        {
            return View("Register");
        }
        [Route("Login")]
        public IActionResult Login()
        {
            return View("Login");
        }
        [Route("Users")]
        public IActionResult Users()
        {
            return View("Users");
        }
        [Route("Emails")]
        public IActionResult Emails()
        {
            return View();
        }
        [Route("EmailSent")]
        public IActionResult EmailSent()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
