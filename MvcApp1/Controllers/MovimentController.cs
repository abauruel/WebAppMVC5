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
    public class MovimentController : Controller
    {
        private readonly EFModel ctx = new EFModel();
        // GET: Moviment
        public ActionResult Index()
        {

            
             return View();
        }

        [HttpPost]
        public ActionResult Index(string data1,string data2)
        {

            CultureInfo culture = new CultureInfo("fr-FR");
            var dateini = DateTime.Parse(data1, culture);
            var dateEnd = DateTime.Parse(data2, culture);
            string message = "";
            if (dateini < dateEnd)
            {

                dateEnd = dateEnd.AddHours(23).AddMinutes(59).AddSeconds(59);
                List<string> dates = new List<string>();
                IEnumerable<Moviment> moviment = from moviments in ctx.Moviments where moviments.Date >= dateini && moviments.Date <= dateEnd select moviments;

                if (moviment.Count() > 0)
                {
                    foreach (var item in moviment)
                    {
                        dates.Add(item.Date.ToString());
                    }

                    //return array Dates
                    ViewBag.DATES = dates;

                    //Return average price
                    ViewBag.AVERAGE = moviment.Select(P => P.Price).Average().ToString("C2", culture);

                    //return minor price
                    ViewBag.MIN = moviment.Select(p => p.Price).Min().ToString("C2", culture);

                    //Return time more expensive
                    var maxPrice = moviment.Select(p => p.Price).Max();
                    ViewBag.MAX = maxPrice.ToString("C2", culture);
                    var maxPriceArr = moviment.Where(p => p.Price == maxPrice);
                    ViewBag.TIMEEXPENSIVE = maxPriceArr.Select(p => p.Date).Distinct().First().ToShortTimeString();


                    //return array prices
                    ViewBag.PRICES = moviment.Select(p => p.Price);

                }
                else
                {
                    message = "There is no record for the period informed";
                }

                ViewBag.MESSAGE = message!= "" ? message.ToString():null;
                return View(moviment);
            }
            else
            {
                ViewBag.MESSAGE = "Invalid period";
                return View();
            }
        }

      
    }
}