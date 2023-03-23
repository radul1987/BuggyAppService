using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace BuggyAppService.Controllers
{
    public class TaskResult : Controller
    {
        public IActionResult Index()
        {
            DateTime dtStart = DateTime.Now;
            int timetaken = 25;
            string timeTakenQuery = Request.Query["time"];
            timetaken = (timeTakenQuery != null) ? Int32.Parse(timeTakenQuery) : 5000;
            string pageContent = MakeRequest("test",timetaken).GetAwaiter().GetResult();
            ViewData["PageContent"] = pageContent;
            return View();
        }

         public async Task<string> MakeRequest(String urlquery,int timetaken)
        {
           var  strReturn = "nothing";
            var url = $"https://ratanas.net/SlowPage.aspx";
            var parameters = @"?time="+timetaken;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
           // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(parameters).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                 strReturn = await response.Content.ReadAsStringAsync();
               // var recipeList = JsonConvert.DeserializeObject<RecipeList>(jsonString);

                
            }
            return strReturn;
        }
    }
}
