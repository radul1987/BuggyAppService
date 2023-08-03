using Microsoft.AspNetCore.Mvc;

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
    }

}
