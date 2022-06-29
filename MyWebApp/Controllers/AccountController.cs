using MyWebApp.Insfrastructure.Context;
using MyWebApp.Models.Entities;
using MyWebApp.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyWebApp.Controllers
{
    public class AccountController : Controller
    {
        MyDbContext context = new MyDbContext();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Password != model.PasswordConfirmed)
            {
                //Add Error Validation Message Summary
                return View(model);
            }
            var role = context.Roles.FirstOrDefault(x => x.RoleName == model.RoleName);
            if (role == null)
            {
                //Rol var mı yok mu kontrolü
                //Add Error Validation Message Summary
                return View(model);
            }
            User user = new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                Role = role
            };
            //eklenen User'ın var olup olmadığı kontrolü
            context.Entry<User>(user).State = EntityState.Added;
            context.SaveChanges();
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string ReturnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user == null)
            {
                //Add Error Validation Message Summary
                return View(model);
            }
            if (user.Password != model.Password)
            {
                //Add Error Validation Message Summary
                return View(model);
            }
            //FormsAuthentication.SetAuthCookie(user.Username, false);
            //yetkilendirme bileti oluşturuyorum.
            var authTicket = new FormsAuthenticationTicket(
                              1,
                              user.Username,
                              DateTime.Now,
                              DateTime.Now.AddMinutes(20), // expire
                              false,
                              user.Role.RoleName,
                              "/");
            //sonrasında bu bileti cookie ye atıyorum.
            //FormsAuthenticationTicket nesnesini encrpyt fonksiyonu(metodu) ile string bir ifadeye çeviriyorum.
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
              FormsAuthentication.Encrypt(authTicket));
            //Cookie yi ekliyorum.
            Response.Cookies.Add(cookie);

            if (ReturnUrl == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(ReturnUrl);// "/Home/About"
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
            return RedirectToAction("login");
        }
    }
}