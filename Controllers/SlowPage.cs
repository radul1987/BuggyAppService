using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BuggyAppService.Controllers
{
    public class SlowPage : Controller
    {

        public IActionResult Index()
        {
            string timeFromURL = null;
            //  string timeTakenQuery = Request.Query["time"];
            string timeTakenQuery2 = Request.Path.ToString();


            if (timeTakenQuery2 != null && timeTakenQuery2.Contains("="))
            {
                timeFromURL = timeTakenQuery2.Split('=')[1];
            }


            if (timeFromURL != null)
            {
                DateTime dateStart = DateTime.Now;
                int timetaken = 25;


                timetaken = (timeFromURL != null) ? Int32.Parse(timeFromURL) : 5000;

                System.Threading.Thread.Sleep(timetaken);
                //string x = Environment.GetEnvironmentVariable("Test10");
                ViewData["TimeTaken"] = "TimeTaken " + DateTime.Now.Subtract(dateStart).TotalSeconds + " sec";
            }
            return View();
        }
    }



}


