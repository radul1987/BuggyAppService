using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;

namespace BuggyAppService.Controllers
{
    public class SlowPage : Controller
    {
        public IActionResult Index()
        {
            DateTime dtStart = DateTime.Now;
            int timetaken = 25;
       
            string timeTakenQuery = Request.Query["time"];
            timetaken = (timeTakenQuery != null) ? Int32.Parse(timeTakenQuery) : 5000;

            System.Threading.Thread.Sleep(timetaken);
            //string x = Environment.GetEnvironmentVariable("Test10");
            ViewData["TimeTaken"] = "Page took " + DateTime.Now.Subtract(dtStart).TotalMilliseconds + " ms to load ";

            return View();
        }
    }
}
