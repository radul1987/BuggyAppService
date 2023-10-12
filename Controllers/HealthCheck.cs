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

            string healthCheckIssue = "on";
            try
            {
                healthCheckIssue = System.Environment.GetEnvironmentVariables()["HealthCheckIssue"].ToString();

            }
            catch (Exception ex)
            {

            }

            if (healthCheckIssue.ToLower() == "on") {
              //  numberOfFailures++;
                throw new Exception("induced health check issue due to the key HealthCheckIssue=true");
            }
          
            return View();
        }
    }
}
