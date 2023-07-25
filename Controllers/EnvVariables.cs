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
                if (e.Value != null)
                {
                    envVariables.Value = e.Value.ToString();
                }
                listEnvVars.Add(envVariables);
            }

           // str.Append($"<table class=\"table\">\r\n  <thead>\r\n    <tr>\r\n      <th scope=\"col\">#</th>\r\n      <th scope=\"col\">First</th>\r\n      <th scope=\"col\">Last</th>\r\n      <th scope=\"col\">Handle</th>\r\n    </tr>\r\n  </thead>\r\n  <tbody>\r\n    <tr>\r\n      <th scope=\"row\">1</th>\r\n      <td>Mark</td>\r\n      <td>Otto</td>\r\n      <td>@mdo</td>\r\n    </tr>\r\n    <tr>\r\n      <th scope=\"row\">2</th>\r\n      <td>Jacob</td>\r\n      <td>Thornton</td>\r\n      <td>@fat</td>\r\n    </tr>\r\n    <tr>\r\n      <th scope=\"row\">3</th>\r\n      <td colspan=\"2\">Larry the Bird</td>\r\n      <td>@twitter</td>\r\n    </tr>\r\n  </tbody>\r\n</table>");
            ViewData["EnvVariables"] = str;



            return View(listEnvVars);
        }
    }
}
