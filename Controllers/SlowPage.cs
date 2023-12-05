using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BuggyAppService.Controllers
{
    public class SlowPage : Controller
    {

        //public IActionResult Index()
        //{
        //    string timeFromURL = null;
        //  //  string timeTakenQuery = Request.Query["time"];
        //    string timeTakenQuery2 = Request.Path.ToString();


        //    if (timeTakenQuery2 != null && timeTakenQuery2.Contains("="))
        //    {
        //         timeFromURL = timeTakenQuery2.Split('=')[1];
        //    }


        //    if (timeFromURL != null)
        //    {
        //        DateTime dateStart = DateTime.Now;
        //        int timetaken = 25;


        //        timetaken = (timeFromURL != null) ? Int32.Parse(timeFromURL) : 5000;

        //        System.Threading.Thread.Sleep(timetaken);
        //        //string x = Environment.GetEnvironmentVariable("Test10");
        //        ViewData["TimeTaken"] = "TimeTaken " + DateTime.Now.Subtract(dateStart).TotalSeconds + " sec";
        //    }
        //    return View();
        //}
        //}


        public IActionResult Index()
        {
            string returnMessage = "OK";

            var postData = new Dictionary<string, string>    {
                {"grant_type", "client_credentials"},
                {"client_id", "APM2CSW3C9ZFP04zRYbFO1Abwo6"},
                {"client_secret", "sxsYCPfmMS734OuMU879"},
                {"scope", "openid"}
            };

            var httpClient = new HttpClient();
            var content = new FormUrlEncodedContent(postData);

            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 ;
                // var response = httpClient.PostAsync("https://dmztst.autostrade.it/mga/sps/oauth/oauth20/token", content).Result;
                var response = httpClient.PostAsync("https://dmztst.autostrade.it/mga/sps/oauth/oauth20/token", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    var deserializedObject = JsonConvert.DeserializeObject<Object>(responseContent);
                    string jsonString = JsonConvert.SerializeObject(deserializedObject, Newtonsoft.Json.Formatting.Indented);

                    returnMessage = jsonString;
                }
                else
                {
                    returnMessage = response.Content.ReadAsStringAsync().Result;
                }

            }
            catch (Exception ex)
            {
                returnMessage = ex.ToString();
            }


            ViewData["TimeTaken"] = "Result  " + returnMessage;
            return View();
        }
    }
}

