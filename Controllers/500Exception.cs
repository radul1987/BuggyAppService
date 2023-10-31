using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BuggyAppService.Controllers
{
    public class _500Exception : Controller
    {
        public IActionResult Index()
        {

            string host = @"https://clicertmap.neo.lan/";
            string certName = @"C:\temp\User1.pfx";
            string password = @"1234";

            try
            {
                X509Certificate2Collection certificates = new X509Certificate2Collection();
                certificates.Import(certName, password, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

                ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(host);
                req.AllowAutoRedirect = true;
                req.ClientCertificates = certificates;
                req.Method = "GET";
                //req.ContentType = "application/x-www-form-urlencoded";
                //string postData = "login-form-type=cert";
                //byte[] postBytes = Encoding.UTF8.GetBytes(postData);
                //req.ContentLength = postBytes.Length;

                //Stream postStream = req.GetRequestStream();
                //postStream.Write(postBytes, 0, postBytes.Length);
                //postStream.Flush();
                //postStream.Close();
                WebResponse resp = req.GetResponse();
                string line = "";
                Stream stream = resp.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream))
                {
                    line = reader.ReadLine();
                    while (line != null)
                    {
                        Console.WriteLine(line);
                        line += reader.ReadLine();
                    }
                }

                stream.Close();
                ViewData["clientCert"] = line;
            }
            catch (Exception e) {
                ViewData["clientCert"] = e.Message;
            }
         
            return View();
        }

        public IActionResult Index2()
        {
            string x = null;
            string y = x.Split('x')[0];
            return View();
        }

        public IActionResult Index3()
        {
            return StatusCode(503);
        }

        public IActionResult Index4()
        {
            return StatusCode(403);
        }
    }

}
