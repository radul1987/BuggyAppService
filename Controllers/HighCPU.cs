using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BuggyAppService.Controllers
{
    public class HighCPU : Controller
    {
        [HttpPost]
        public IActionResult Index(IFormCollection collection)
        {
            string regexMatch = Request.Form["name"].ToString();

            DateTime dtStart = DateTime.Now;
            Regex regex = new Regex(@"(^(?=^.{1,254}$)[a-z0-9!#$%'*/=?^_`{|}~\-&.]+(?:\.[a-z0-9!#$%'*/=?^_`{|}~\-&.]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9]))+(?:\.[a-z]{2,6})?(\.[a-z]{2,6})$)|(^[a-zA-Z0-9\!\#\$\%\&\'\*\-\/\=\?\^_\`\{\|\}\~]+(\.[a-zA-Z0-9\!\#\$\%\&\'\*\-\/\=\?\^_\`\{\|\}\~]+)*@[a-zA-Z0-9]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(\.[a-zA-Z0-9]([a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*\.[a-zA-Z]{2,6}$)|(^(?=^.{1,254}$)(("".+?"")|([0-9a-zA-Z](((\.(?!\.))|([-!#\$%&'\*/=\?\^`\{\}\|~\w]))*[0-9a-zA-Z])*))@(([0-9a-zA-Z][\.])|([0-9a-zA-Z][-A-Z0-9a-z]*[0-9a-zA-Z]\.){1,63})+[a-zA-Z0-9]{2,6}$)|(^[0-9]{1,10}$)");
            //Response.Write(regex.IsMatch("someincorrectemailaddress@ymydomain.c"));
            regex.IsMatch(regexMatch);
            //Response.Write($"<br/> Took {DateTime.Now.Subtract(dtStart).TotalSeconds}seconds");
            ViewData["TimeTaken"] = $"Page Took {DateTime.Now.Subtract(dtStart).TotalSeconds}seconds";
            return View();
        }
        [HttpGet]
        public IActionResult Index()
        {
            DateTime dtStart = DateTime.Now;
            //  string timeTakenQuery = Request.Query["time"];
            string param = Request.Path.ToString();




            if (param != null && param.Contains("Concat"))
            {
                string str = string.Empty;
                for (int i = 0; i < 100000; i++)
                {
                    str += " Hello World";
                }
            }
            ViewData["TimeTaken"] = $"Page Took {DateTime.Now.Subtract(dtStart).TotalSeconds}seconds";
            return View();
        }



}

}
