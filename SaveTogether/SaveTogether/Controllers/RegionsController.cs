using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaveTogether.DAL.Context;
using SaveTogether.DAL.Entities;

namespace SaveTogether.Controllers
{
    public class RegionsController : Controller
    {
        private SaveTogetherContext db = new SaveTogetherContext();

        // GET: Regions
        public ActionResult Index()
        {
            var regions = db.Regions.Include(r => r.Country);
            return View(regions.ToList());
        }

        //public JsonResult GetData()
        //{
        //    List<Region> stations = new List<Region>();
        //    stations.Add(new Region()
        //    {
        //        Id = 1,
        //        Name = "Lenin library",
        //        GeoLat = 37.610489,
        //        GeoLong = 55.752308
        //    });
        //    stations.Add(new Region()
        //    {
        //        Id = 2,
        //        Name = "Alexander garden",
        //        GeoLat = 37.608644,
        //        GeoLong = 55.75226
        //    });
        //    stations.Add(new Region()
        //    {
        //        Id = 3,
        //        Name = "Borovitskaya",
        //        GeoLat = 37.609073,
        //        GeoLong = 55.750509
        //    });

        //    return Json(stations, JsonRequestBehavior.AllowGet);
        //}

        // GET: Regions/Details/0
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = db.Regions.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // GET: Regions/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        // POST: Regions/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Name,CountryId,Population")] Region region)
        {
            if (ModelState.IsValid)
            {
                db.Regions.Add(region);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", region.CountryId);
            return View(region);
        }

        // GET: Regions/Edit/0
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = db.Regions.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", region.CountryId);
            return View(region);
        }

        // POST: Regions/Edit/0
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,CountryId,Population")] Region region)
        {
            if (ModelState.IsValid)
            {
                db.Entry(region).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", region.CountryId);
            return View(region);
        }

        // GET: Regions/Delete/0
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = db.Regions.Find(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        // POST: Regions/Delete/0
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Region region = db.Regions.Find(id);
            db.Regions.Remove(region);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
