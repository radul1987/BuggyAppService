using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BuggyAppService.Controllers
{
    public class HighMemory : Controller
    {
        static ConcurrentDictionary<int, string> myDictionnary = new ConcurrentDictionary<int, string>();
        public IActionResult Index()
        {
            DateTime dateStart = DateTime.Now;

            string param = Request.Path.ToString();
            // string hugeString = new string('*', 510000);
            int i = myDictionnary.Count;
            int x = myDictionnary.Count;
            while (i < x + 200)
            {
                string hugeString = new string('*', 510000);
                myDictionnary.TryAdd(i, hugeString);

                i++;
            }
            long processMemory = Process.GetCurrentProcess().WorkingSet64;
            long privateMemory = Process.GetCurrentProcess().PrivateMemorySize64;

            ViewData["TimeTaken"] = $"Time Taken {DateTime.Now.Subtract(dateStart).TotalSeconds}seconds - dictionary size {myDictionnary.Count}";
            //  https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.privatememorysize64?view=net-7.0
            ViewData["PhysicalMemory"] = $" Physical Memory usage = {ConvertBytesToMegabytes(processMemory)}Mb";
            ViewData["PrivateMemory"] = $" Private Memory usage = {ConvertBytesToMegabytes(privateMemory)}Mb";


            //ViewData["PhysicalMemory"] = Process.GetCurrentProcess().PeakPagedMemorySize64;
            //ViewData["PhysicalMemory"] = Process.GetCurrentProcess().PeakVirtualMemorySize64;
            //ViewData["PhysicalMemory"] = Process.GetCurrentProcess().PeakWorkingSet64;
            return View();
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }



    }

}
