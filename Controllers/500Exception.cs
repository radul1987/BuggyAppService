using Microsoft.AspNetCore.Mvc;

namespace BuggyAppService.Controllers
{
    public class _500Exception : Controller
    {
        public IActionResult Index()
        {
            throw new ApplicationException("Throwing new application exception");
            return View();
        }
    }
}
