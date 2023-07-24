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

            ThisIsARecursiveFunctionUsedToTriggerAStackOVerflow();

            return View();
        }
        private void ThisIsARecursiveFunctionUsedToTriggerAStackOVerflow()
        {
            //try
            //{
            for (int i = 0; i < 1000; i++)
            {
                ThisIsARecursiveFunctionUsedToTriggerAStackOVerflow();
            }
            //}
            //catch (StackOverflowException ex)
            //{
            //   // Response.Write(ex.Message + "<-*******->" + ex.StackTrace);
            //}
            //catch (Exception ex)
            //{
            //   // Response.Write(ex.Message);
            //}
        }
    }
}
