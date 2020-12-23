using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using LaSumka.Controllers;
using Xunit;
using LaSumka.Models;
using Microsoft.EntityFrameworkCore;

namespace LaSumka.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void RegisterIsNotNull()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LaSumka.Models.BagContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LaSumkaDB;Trusted_Connection=True;");


            using (var db = new LaSumka.Models.BagContext(optionsBuilder.Options))
            {
                using (var controller = new HomeController(db))
                {

                    var result = controller.Register();

                    Assert.NotNull(result);
                }
            }
        }
        [Fact]
        public void LoginIsNotNull()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LaSumka.Models.BagContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LaSumkaDB;Trusted_Connection=True;");


            using (var db = new LaSumka.Models.BagContext(optionsBuilder.Options))
            {
                using (var controller = new HomeController(db))
                {

                    var result = controller.Login();

                    Assert.NotNull(result);
                }
            }
        }
    }
}
