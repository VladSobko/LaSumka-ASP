using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using LaSumka.Models;
namespace LaSumka.Controllers
{
    public class UserController : Controller
    {
        private BagContext db;
        public UserController(BagContext context)
        {
            db = context;
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult Bags()
        {

            var bags = db.Bags.Include(u => u.Categories);
            ViewBag.Bags = bags;

            User user = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            ViewBag.User = user;
            return View(bags.ToList());

        }
        [HttpPost]
        [Authorize(Roles = "user")]
        public ActionResult AddToCart( int Id)
        {
            if (ModelState.IsValid)
            {
                
                    User user = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);

                    ShopCart cart = new ShopCart
                    {

                        Count = 1,
                        Date = DateTime.Now,
                        BagId = Id,
                        UserId = user.Id,


                    };
                    db.ShopCarts.Add(cart);
                    db.SaveChanges();
                    return RedirectToAction("Carts", "User", new { area = "" });
                
            }

            return View();

        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult Carts()
        {
            User user = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            var carts = db.ShopCarts.Include(u => u.Bag).Include(u => u.User).Where(p => p.UserId == user.Id);
            ViewBag.Carts = carts;
            return View(carts.ToList());

        }
      
        [HttpPost]
        [Authorize(Roles = "user")]
        public ActionResult Order(int Id)
        {
            ShopCart cart = db.ShopCarts.FirstOrDefault(p => p.Id == Id);
            Order order = new Order
            {

                Count = 1,
                Date = DateTime.Now,
                BagId = cart.BagId,
                UserId = cart.UserId,


            };
            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("OrderReady", "User", new { area = "" });

        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult OrderReady()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult Contact()
        {
            User user = db.Users.FirstOrDefault(p => p.RoleId == 1);
            ViewBag.User = user;

            return View();
        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult Bag(int id)
        {
            var products = db.Bags.Include(u => u.Categories);
            var product = products.Where(p => p.Id == id);
            return View(product.ToList());
        }
    }

}
