using BuggyAppService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BuggyAppService.Controllers
{
    public class UpcomingFeatures : Controller
    {
        private readonly ILogger<HomeController> _logger;



        public IActionResult Index()
        {
            return View();
        }
    }
}