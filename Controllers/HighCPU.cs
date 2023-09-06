using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BuggyAppService.Controllers
{
    public class HighCPU : Controller
    {
        public IActionResult Index()
        {
            DateTime dtStart = DateTime.Now;
            //  string timeTakenQuery = Request.Query["time"];
            string param = Request.Path.ToString();

            if (param != null && param.Contains("concat"))
            {
                string str = string.Empty;
                for (int i = 0; i < 60000; i++)
                {
                    str += " Hello World";
                }
                ViewData["TimeTaken"] = $"Page Took {DateTime.Now.Subtract(dtStart).TotalSeconds}seconds";
            }
          
            return View();
        }



}

}
