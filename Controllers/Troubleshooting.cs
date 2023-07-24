using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;

namespace BuggyAppService.Controllers
{
    public class Troubleshooting : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Crash()
        {
            return View();
        }

        public IActionResult SlowRequest()
        {
            return View();
        }
    }
}
