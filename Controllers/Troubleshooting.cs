using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;

namespace BuggyAppService.Controllers
{
    /// <summary>
    /// Controller for troubleshooting guides 
    /// </summary>
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

        public IActionResult HighCPU()
        {
            return View();
        }
        public IActionResult SNAT()
        {
            return View();
        }

        public IActionResult HighMemory()
        {
            return View();
        }

    }
}
