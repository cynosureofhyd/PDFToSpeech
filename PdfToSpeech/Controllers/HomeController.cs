using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PdfToText;

namespace PdfToSpeech.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult File()
        {
            return View();
        }

        [HttpPost]
        public ActionResult File(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                ExtractText(fileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }

            return RedirectToAction("File");
        }

        private static void ExtractText(string file)
        {
            try
            {
                string fileName = "App_Data/uploads/{0}";
                string filePath = System.Web.HttpContext.Current.Server.MapPath(String.Format(fileName, file));
                
                //if (!System.IO.File.Exists(filePath))
                //{
                //    file = Path.GetFullPath(file);
                //    if (!System.IO.File.Exists(file))
                //    {
                //        Console.WriteLine("Please give in the path to the PDF file.");
                //    }
                //}

                PDFParser pdfParser = new PDFParser();
                int index = filePath.IndexOf("Home");
                filePath = filePath.Replace("\\Home", "");
                pdfParser.ExtractText(filePath, Path.GetFullPath(filePath) + ".txt");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
