using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Extensions.Primitives;
using BuggyAppService.Models;

namespace BuggyAppService.Controllers
{
    public class EnvVariables : Controller
    {
        public IActionResult Index()
        {
            List<Models.EnvVariables> listEnvVars = new List<Models.EnvVariables>();
            StringBuilder str = new StringBuilder();

            foreach (DictionaryEntry e in System.Environment.GetEnvironmentVariables())
            {
                //  str.Append("<p>" +e.Key + ":" + e.Value + "</p>");
                Models.EnvVariables envVariables = new Models.EnvVariables();
                envVariables.Key = e.Key.ToString();
                if (!e.Key.ToString().ToLower().Contains("key") && !e.Key.ToString().ToLower().Contains("secret") 
                    && !e.Value.ToString().ToLower().Contains("key") && !e.Value.ToString().ToLower().Contains("secret")
                    && !e.Key.ToString().ToLower().Contains("connection"))
                {
                    if (e.Value != null)
                    {
                        envVariables.Value = e.Value.ToString();
                    }
                    listEnvVars.Add(envVariables);
                }
            }

            ViewData["EnvVariables"] = str;



            return View(listEnvVars);
        }
    }
}
