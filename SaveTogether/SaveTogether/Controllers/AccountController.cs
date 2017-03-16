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
    public class AccountController: Controller
    {
        private SaveTogetherContext db = new SaveTogetherContext();
        private AuthorizedPersonManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AuthorizedPersonManager>();
            }
        }

        public async Task<ActionResult> Register()
        {
            return View();
        }

        public async Task<ActionResult> Subscribe()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                AuthorizedPerson user = new Customer { UserName = model.UserName, Email = model.Email, SecondName = model.SecondName, DateOfBirth = model.DateOfBirth };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        public async Task<ActionResult> Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AuthorizedPerson user = await UserManager.FindAsync(model.Email, model.Password);
                if(user == null)
                {
                    ModelState.AddModelError("", "Wrong login or password.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public async Task<ActionResult> LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        public async Task<ActionResult> Cabinet()
        {
            Customer customer = (Customer) await UserManager.FindByNameAsync(User.Identity.Name);
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
                return RedirectToAction("Index", "Home");
            }
            return View(customer);
        }
    }
}
