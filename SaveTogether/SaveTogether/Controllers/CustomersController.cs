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
    public class CustomersController : Controller
    {
        private AuthorizedPersonManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AuthorizedPersonManager>();
            }
        }
        
        public ActionResult Index()
        {
            SaveTogetherContext db = new SaveTogetherContext();
            return View(db.Customers.ToList());
        }

        public ActionResult Details(string id)
        {
            SaveTogetherContext db = new SaveTogetherContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaveTogetherContext db = new SaveTogetherContext();
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,UserName,SecondName,DateOfBirth")]Customer customer)
        {
            SaveTogetherContext db = new SaveTogetherContext();
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            SaveTogetherContext db = new SaveTogetherContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
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
