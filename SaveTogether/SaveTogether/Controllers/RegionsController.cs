using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SaveTogether.DAL.Context;
using SaveTogether.DAL.Entities;

namespace SaveTogether.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RegionsController : Controller
    {
        private SaveTogetherContext db = new SaveTogetherContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.Regions.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = await db.Regions.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Population")] Region region)
        {
            if (ModelState.IsValid)
            {
                db.Regions.Add(region);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(region);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = await db.Regions.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Descripion,Population")] Region region)
        {
            if (ModelState.IsValid)
            {
                db.Entry(region).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(region);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Region region = await db.Regions.FindAsync(id);
            if (region == null)
            {
                return HttpNotFound();
            }
            return View(region);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Region region = await db.Regions.FindAsync(id);
            db.Regions.Remove(region);
            await db.SaveChangesAsync();
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
