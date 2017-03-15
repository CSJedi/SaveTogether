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
using SaveTogether.Services;

namespace SaveTogether.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DonationsController : Controller
    {
        private SaveTogetherContext db = new SaveTogetherContext();

        public async Task<ActionResult> Index()
        {
            var donations = await db.Donations.Include(d => d.Region).ToListAsync();
            return View(donations);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = await db.Donations.FindAsync(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }
        public async Task<ActionResult> Create()
        {
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Sum,RegionId")] Donation donation)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("CreateToken", donation);
            }

            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", donation.RegionId);
            return View(donation);
        }
        public async Task<ActionResult> CreateToken(Donation donation)
        {
            var a = donation;
            return View(donation);
        }
        [HttpPost]
        public async Task<ActionResult> CreateToken(string token, Donation donation)
        {
            donation.Token = token;
            var service = new WorldPayService();
            //TODO: test payment, handle null ref exp
            if (service.MakePayment(donation))
            {
                //TODO: make saving donate
                //db.Donations.Add(donation);
                //await db.SaveChangesAsync();
            }
            //TODO: create successPayment view
            //return Redirect(SuccessPayment);\
            return View();
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = await db.Donations.FindAsync(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", donation.RegionId);
            return View(donation);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Sum,OperationDateTime,RegionId,PersonId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", donation.RegionId);
            return View(donation);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donation donation = await db.Donations.FindAsync(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Donation donation = await db.Donations.FindAsync(id);
            db.Donations.Remove(donation);
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
