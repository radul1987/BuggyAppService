using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BuggyAppService.Controllers
{
    public class ClientCertAuth : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormFile postedFile)
        {
            try
            {
                if (postedFile == null || postedFile.Length == 0)
                    return BadRequest("No file selected for upload...");

                string fileName = Path.GetFileName(postedFile.FileName);
                string contentType = postedFile.ContentType;
                string host = Request.Form["url"];
                string password = Request.Form["certpass"];

                using (var stream = new MemoryStream())
                {
                    postedFile.CopyTo(stream);
                    byte[] bytes = stream.ToArray();
                    X509Certificate2Collection certificates = new X509Certificate2Collection();
                    certificates.Import(bytes, password, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

                    ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(host);
                    req.AllowAutoRedirect = true;
                    req.ClientCertificates = certificates;
                    string certSubject = certificates[0].Subject;
                    req.Method = "GET";

                    WebResponse resp = req.GetResponse();
                    HttpWebResponse RespStatusCode = (HttpWebResponse)resp;
                    var x = (int)RespStatusCode.StatusCode;

                    //\                   string line = "";
                    //                    Stream respStream = resp.GetResponseStream();
                    //                    using (StreamReader reader = new StreamReader(respStream))
                    //                    {
                    //                        line = reader.ReadLine();
                    //                        while (line != null)
                    //                        {
                    //                            Console.WriteLine(line);
                    //                            line += reader.ReadLine();
                    //                           //break;
                    //                        }
                    //                        respStream.Close();
                    //                        ViewData["RequestStatus"] = line;
                    //                    }
                    ViewData["RequestStatus"] = x + " "+ certSubject;
                }
            }
            catch (Exception e)
            {
                ViewData["RequestStatus"] = e.Message;
            }
            return View();
        }
    }
}