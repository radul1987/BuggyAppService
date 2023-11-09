using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BuggyAppService.Controllers
{
    public class _500Exception : Controller
    {
        public IActionResult Index()
        {     
            return View();
        }

        public IActionResult Index2()
        {
            string x = null;
            string y = x.Split('x')[0];
            return View();
        }

        public IActionResult Index3()
        {
            return StatusCode(503);
        }

        public IActionResult Index4()
        {
            return StatusCode(403);
        }
    }

}
