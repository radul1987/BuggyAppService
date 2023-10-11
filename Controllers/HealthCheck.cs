using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;

namespace BuggyAppService.Controllers
{
    public class HealthCheck : Controller
    {
        static int numberOfFailures = 0;
        public IActionResult Index()
        {

            if (numberOfFailures < 10) {
                numberOfFailures++;
                throw new Exception("induced 10 errors for health check feature when app is starting");
            }
          
            return View();
        }
    }
}
