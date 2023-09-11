using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;

namespace BuggyAppService.Controllers
{
    public class HealthCheck : Controller
    {

        public IActionResult Index()
        {
          
            return View();
        }
    }
}
