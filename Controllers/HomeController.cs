using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestTaskDotnet.Models;

namespace TestTaskDotnet.Controllers
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

        public IActionResult RegisterForm()
        {
            return View();
        }
        public IActionResult SigIn()
        {
            return View();
        }

        public IActionResult Request()
        {
            return View();
        }
        public IActionResult RequestHistory()
        {
            return View();
        }
        public IActionResult MyRequests()
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