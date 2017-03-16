using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SaveTogether.DAL.Context;
using SaveTogether.DAL.Entities;
using SaveTogether.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace SaveTogether.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private SaveTogetherContext db = new SaveTogetherContext();
        private AuthorizedPersonManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AuthorizedPersonManager>();
            }
        }
        
        public async Task<ActionResult> Index()
        {
            return View(await db.Customers.ToListAsync());
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,UserName,SecondName,DateOfBirth")]Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm()
        {
            AuthorizedPerson user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("LogOff", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
