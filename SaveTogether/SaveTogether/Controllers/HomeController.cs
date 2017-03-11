using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaveTogether.DAL.Context;

namespace SaveTogether.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Map()
        {
            SaveTogetherContext db = new SaveTogetherContext();
            return View(db.Regions.ToList());
        }

        public ActionResult Contact()
        {
            return View();
        }

        public void TransferToMap()
        {
            RedirectToRoute(Map());
            //Server.Transfer("Page2.aspx", true);
        }
    }
}