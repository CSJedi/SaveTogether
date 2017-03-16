﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SaveTogether.DAL.Context;
using SaveTogether.DAL.Entities;
using SaveTogether.Services;
using SaveTogether.Models;

namespace SaveTogether.Controllers
{
    public class DonationsController : Controller
    {
        private AuthorizedPersonManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AuthorizedPersonManager>();
            }
        }
        private SaveTogetherContext db = new SaveTogetherContext();

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Index()
        {
            var donations = await db.Donations.Include(d => d.Region).ToListAsync();
            return View(donations);
        }

        [Authorize(Roles = "Admin")]
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
            //TODO: Read this ref https://developer.worldpay.com/jsonapi/docs/testing
            return View(donation);
        }
        [HttpPost]
        public async Task<ActionResult> CreateToken(string token, Donation donation)
        {
            donation.Token = token;
            WorldPayService service = new WorldPayService();
            //TODO: test payment, handle null ref exp
            if (service.MakePayment(donation))
            {
                //TODO: make saving donate
                donation.OperationDateTime = DateTime.Now;
                Customer customer = (Customer)await UserManager.FindByNameAsync(User.Identity.Name);
                if (customer != null)
                    donation.Person = customer;

                db.Donations.Add(donation);
                await db.SaveChangesAsync();
            }
            //TODO: create successPayment view
            return RedirectToAction("SuccessDonate");
            //return View();
        }

        public async Task<ActionResult> SuccessDonate()
        {
            return View();
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
