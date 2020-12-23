using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LaSumka.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace LaSumka.Controllers
{
    public class HomeController : Controller
    {
        private BagContext db;
        public int localId;
        public HomeController(BagContext context)
        {
            db = context;
        }
      
        [HttpGet]
        public ActionResult Index()
        {
            var bags = db.Bags.Include(u => u.Categories);
            ViewBag.Bags = bags;
            return View(bags.ToList());
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User userform)
        {
            if (ModelState.IsValid)
            {
              
                User user = new User
                {
                    Surname = userform.Surname,
                    Name = userform.Name,
                    Email = userform.Email,
                    Password = userform.Password,
                    Phone = userform.Phone,
                    RoleId = 2

                };
                db.Users.Add(user);
                Authenticate(user);
                db.SaveChanges();

                return RedirectToAction("Bags", "User", new { id = userform.Id });
            }
            else return View(userform);


        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {

            User user1 = db.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);



            if (user1.RoleId == 1)
            {

                Authenticate(user1);
                 return RedirectToAction("Bags", "Admin", new { area = "" });

            }
            else if (user1.RoleId == 2)
            {

                Authenticate(user1);
                return RedirectToAction("Bags", "User");

            }
            return View(user);




        }
        public ActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { area = "" });
        }
     
        public Task Authenticate(User user)
        {
            string Rolename = "Null";

            if (user != null)
            {
                if (user.RoleId == 1) { Rolename = "admin"; }
                else if (user.RoleId == 2) { Rolename = "user"; }

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,  Rolename)
                };
                // создаем объект ClaimsIdentity
                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                // установка аутентификационных куки
                return HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }
            else return null;




        }
    }
}
