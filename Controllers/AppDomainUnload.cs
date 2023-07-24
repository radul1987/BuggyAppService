﻿using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

namespace BuggyAppService.Controllers
{
    public class AppDomainUnload : Controller
    {
        public IActionResult Index()
        {
            try
            {
                if (Request.Path.ToString().Contains("createFile"))
                {
                    btnCreateFile();
                }
            }
            catch {
            
            }
            return View();
        }



        public void btnCreateFile()
        {
            string path = @"MyTest";
            Random rnd = new Random();
            path += rnd.Next() + ".txt";

            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = System.IO.File.Create(path))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                //// Open the stream and read it back.
                //using (StreamReader sr = System.IO.File.OpenText(path))
                //{
                //    string s = "";
                //    while ((s = sr.ReadLine()) != null)
                //    {
                //        Console.WriteLine(s);
                //    }
                //}
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        
        }
    }
}
