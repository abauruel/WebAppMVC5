using MvcApp1.DAL;
using MvcApp1.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApp1.Controllers
{
    public class UploadFileController : Controller
    {
        private readonly EFModel ctx = new EFModel();
        // GET: UploadFile
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            if (file?.ContentLength > 0)
            {
                try { 
                List<Moviment> Lmoviment = new List<Moviment>();
                CultureInfo cultureInfo = new CultureInfo("fr-FR");

                using (StreamReader str = new StreamReader(file.InputStream))
                {
                    string line;

                    while ((line = str.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        if (data[0] != "Date" && data[1] != "Price")
                        {
                            Moviment moviment = new Moviment
                            {
                                Date = DateTime.Parse(data[0], cultureInfo),
                                Price = double.Parse(data[1]),
                            };
                            Lmoviment.Add(moviment);
                        }
                    }
                }

                    

                foreach (var item in Lmoviment)
                {
                    ctx.Moviments.Add(item);
                    ctx.SaveChanges();
                }
                    ViewBag.SUCCESS = "Success";

                return View();
            }
                catch(Exception message)
                {
                    ViewBag.ERROR = "failed to import the file";
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}