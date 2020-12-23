using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using LaSumka.Models;

namespace LaSumka.Controllers
{
    public class AdminController : Controller
    {
        private BagContext db;
        private readonly IHostingEnvironment _appEnvironment;


        public AdminController(BagContext context, IHostingEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Bags()
        {

            var bags = db.Bags.Include(u => u.Categories);
            ViewBag.Bags = bags;
            return View(bags.ToList());

        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Bag(int id)
        {
            var bags = db.Bags.Include(u => u.Categories);
            var bag = bags.Where(p => p.Id == id);
            return View(bag.ToList());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Users()
        {

            IEnumerable<User> users = db.Users.Where(p => p.RoleId == 2);
            ViewBag.Users = users;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CategoriesList()
        {
            // localId = id;
            // ViewBag.Id = id;
            IEnumerable<Categories> category = db.Categories;
            ViewBag.Category = category;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult NewCategory()
        {

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult NewCategory(Categories categoryform)
        {

            Categories category = new Categories
            {

                Name = categoryform.Name,


            };
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Bags", "Admin", new { area = "" });

        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Orders()
        {
            var orders = db.Orders.Include(u => u.Bag).Include(u => u.User);
            ViewBag.Orders = orders;
            return View(orders.ToList());
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddBag()
        {

            IEnumerable<Categories> categories = db.Categories;

            ViewBag.Categories = categories;


            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddBag(string Name,  IFormFile Photo, float Price, string Description, bool Available, bool isFavourite, int CategoriesId)
        {
            string path = "/images/" + Photo.FileName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                Photo.CopyTo(fileStream);
            }

            Bag bag = new Bag
            {

                Name = Name,
               
                Photo = path,
                               
                Price = Price,

                Description = Description,

                Available = Available,

                isFavourite = isFavourite,
                
               CategoriesId = CategoriesId,


            };
            db.Bags.Add(bag);
            db.SaveChanges();
            return RedirectToAction("Bags", "Admin", new { area = "" });


            // return View(productform);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditCategory(int Id)
        {
            Categories category = db.Categories.Find(Id);
            ViewBag.Category = category;

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditCategory(Categories categoryform)
        {
            Categories old_category = db.Categories.FirstOrDefault(p => p.Id == categoryform.Id);
            old_category.Name = categoryform.Name;
            db.SaveChanges();
            return RedirectToAction("CategoriesList", "Admin", new { area = "" });
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteCategory(int Id)
        {
            Categories category = db.Categories.FirstOrDefault(p => p.Id == Id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("CategoriesList", "Admin", new { area = "" });
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteBag(int Id)
        {
            Bag product = db.Bags.FirstOrDefault(p => p.Id == Id);
            db.Bags.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Bags", "Admin", new { area = "" });

        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditBag(int Id)
        {
            var products = db.Bags.Include(u => u.Categories);
            var product = products.FirstOrDefault(p => p.Id == Id);
            var categories = db.Categories;
            ViewBag.Bag = product;
            ViewBag.Categories = categories;
            return View(product);

        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditBag(string Name, IFormFile Photo, float Price, string Description, bool Available, bool isFavourite, int CategoriesId, int Id)
        {

            Bag old_bag = db.Bags.FirstOrDefault(p => p.Id == Id);
            if (Photo != null)
            {

                string path = "/images/" + Photo.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
                old_bag.Name = Name;
                old_bag.Photo = path;
                old_bag.Price = Price;
                old_bag.Description = Description;
                old_bag.Available = Available;
                old_bag.isFavourite = isFavourite;
                old_bag.CategoriesId = CategoriesId;

            }
            else
            {
                old_bag.Name = Name;
                old_bag.Photo = old_bag.Photo;
                old_bag.Price = Price;
                old_bag.Description = Description;
                old_bag.Available = Available;
                old_bag.isFavourite = isFavourite;
                old_bag.CategoriesId = CategoriesId;
            }

            db.SaveChanges();
            return RedirectToAction("Bags", "Admin", new { area = "" });



        }

    }
}
