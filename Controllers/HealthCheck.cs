using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;

namespace BuggyAppService.Controllers
{
    public class HealthCheck : Controller
    {
       // static int numberOfFailures = 0;
        public IActionResult Index()
        {

            string healthCheckIssue = "false";
            try
            {
                healthCheckIssue = System.Environment.GetEnvironmentVariables()["StartupIssue"].ToString();

            }
            catch (Exception ex)
            {

            }

            if (healthCheckIssue.ToLower() == "true") {
              //  numberOfFailures++;
                throw new Exception("induced health check issue due to the key HealthCheckIssue=true");
            }
          
            return View();
        }
    }
}
