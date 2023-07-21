using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace BuggyAppService.Controllers
{
    public class EnvVariables : Controller
    {
        public IActionResult Index()
        {
            StringBuilder str = new StringBuilder();

            foreach (DictionaryEntry e in System.Environment.GetEnvironmentVariables())
            {
                str.Append("<p>" +e.Key + ":" + e.Value + "</p>");
            }
            ViewData["EnvVariables"] = str;

            return View();
        }
    }
}
