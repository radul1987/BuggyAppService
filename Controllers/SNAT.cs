using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace BuggyAppService.Controllers
{
    public class SNAT : Controller
    {

        //[HttpPost]
        //public IActionResult Index(IFormCollection collection)
        //{
        //    //
        //    string specificURL = Request.Form["name"].ToString();

        //    DateTime dtStart = DateTime.Now;
        //    int timetaken = 25;
        //    string timeTakenQuery = Request.Query["time"];

        //    if (timeTakenQuery != null)
        //    {
        //        timetaken = (timeTakenQuery != null) ? Int32.Parse(timeTakenQuery) : 5000;
        //        string pageContent = MakeRequest(specificURL, timetaken).GetAwaiter().GetResult();
        //        ViewData["PageContent"] = pageContent;
        //    }
        //    return View();
        //}
        [HttpGet]
        public IActionResult Index()
        {


            //getting the url from environment variables(app service config key) 
            string url = null;
            try
            {
                url = System.Environment.GetEnvironmentVariables()["slowrequest"].ToString();

            }
            catch (Exception ex)
            {

            }
            if (url == null) {
                ViewData["PageContent"] = "The url is not specified. Please add the key 'slowrequest'='https://myslowrequesturl.com' .You can use https://ratanas.net/SlowPage.aspx?time=30000 for testing";
                //}
                return View();

            }


            string param = Request.Path.ToString();

            if (param != null && !param.Contains("execute"))
            {
                ViewData["PageContent"] = "Use the below button to make an outbound query to: " + url;
                //}
                return View();
            }
            //this code is when we want to make the repro using AB linux
            //ab - r - n 10000 - c 500 - s 120 https://buggyappservice.azurewebsites.net/TaskResult?time=30000
            //DateTime dtStart = DateTime.Now;
            //int timetaken = 25;
            //string timeTakenQuery = Request.Query["time"];

            //if (timeTakenQuery != null)
            //{
            //    timetaken = (timeTakenQuery != null) ? Int32.Parse(timeTakenQuery) : 5000;
            string pageContent = MakeRequest(url).GetAwaiter().GetResult();
                ViewData["PageContent"] = pageContent;
            //}
            return View();
        }

         public async Task<string> MakeRequest(String url)
        {
           var  strReturn = "nothing";
            var urlrequest = (url != null)? url : $"https://ratanas.net/SlowPage.aspx";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlrequest);
           // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(urlrequest).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                 strReturn = await response.Content.ReadAsStringAsync();              
            }
            return strReturn;
        }
    }
}
