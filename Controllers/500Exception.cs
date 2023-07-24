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
            throw new ApplicationException("Throwing new application exception");
            return View();
        }
    }

}
