using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SaveTogether.DAL.Context;

namespace SaveTogether.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            return View();
        }

        public async Task<ActionResult> Map()
        {
            SaveTogetherContext db = new SaveTogetherContext();
            return View(await db.Regions.ToListAsync());
        }

        public async Task<ActionResult> Contact()
        {
            return View();
        }

        public async Task<ActionResult> Donate()
        {
            return View();
        }
    }
}