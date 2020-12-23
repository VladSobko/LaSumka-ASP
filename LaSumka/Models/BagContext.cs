using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaSumka.Models
{
    public class BagContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ShopCart> ShopCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Bag> Bags { get; set; }
        public DbSet<Categories> Categories { get; set; }

        public BagContext(DbContextOptions<BagContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "vlad@gmail.com";
            string adminPassword = "1234";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Surname = "Sobko", Name = "Vlad", Phone = "0978756344", Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
        }
    }
}
