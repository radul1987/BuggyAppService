using Microsoft.AspNetCore.Mvc;

namespace BuggyAppService.Controllers
{
    public class StackOverflowController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Index2()
        {

            RecursiveMethod();

            return View();
        }
        private void RecursiveMethod()
        {

            for (int i = 0; i < 1500; i++)
            {
                RecursiveMethod();
            }

        }
    }
}
