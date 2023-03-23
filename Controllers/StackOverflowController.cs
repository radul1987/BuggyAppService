using Microsoft.AspNetCore.Mvc;

namespace BuggyAppService.Controllers
{
    public class StackOverflowController : Controller
    {
        public IActionResult Index()
        {
         //   throw new OutOfMemoryException("svetooutofmemory");
            //try
            //{
            ThisIsARecursiveFunctionUsedToTriggerAStackOVerflow();
            //}
            //catch (StackOverflowException ex)
            //{

            //}
            //catch (Exception ex)
            //{

            //}
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
