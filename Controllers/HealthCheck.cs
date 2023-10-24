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

            string healthCheckIssue = "off";
            try
            {
                healthCheckIssue = System.Environment.GetEnvironmentVariables()["HealthCheckIssue"].ToString();

            }
            catch (Exception ex)
            {

            }

            if (healthCheckIssue.ToLower() == "true") {
              //  numberOfFailures++;
                throw new Exception("induced health check issue due to the key HealthCheckIssue=true");
            }
            else if(healthCheckIssue.ToLower() != "off")
            {
                numberOfFailures++;
                if (numberOfFailures > Convert.ToInt32( healthCheckIssue))
                {
                    numberOfFailures = 0;

                }
                else
                {
                    throw new Exception("induced health check issue due to the key HealthCheckIssue=numberoffailures");
                }

            }


            return View();
        }
    }
}
