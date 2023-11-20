using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SelectPdf;
using System.IO;

namespace BuggyAppService.Controllers
{
    public class ConvertPDF : Controller
    {
        public IActionResult Index()
        {


            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();

            string pageContent = @"
                                    <!DOCTYPE html>
                                    <html>
                                    <body>
                                    <h2>HTML Images</h2>
                                    <p>HTML test image to convert to PDF :</p>
                                    <img src=""{varBaseUrl}/images/FREB.png"" alt="""" >
                                    </body>
                                    </html>
                                    ";

            pageContent = pageContent.Replace("{varBaseUrl}", "https://buggyappservice.azurewebsites.net");

            using (FileStream fs = System.IO.File.Create("test.txt"))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(pageContent);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(pageContent);
            doc.Save("test.pdf");
            doc.Close();

            return View();
        }

    }

}
