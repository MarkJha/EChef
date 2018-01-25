using eCafe.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCafe.Infrastructure.Context
{
    public class ECafeContext : DbContext
    {
        public ECafeContext(DbContextOptions<ECafeContext> options) : base(options)
        {
        }

        public DbSet<MainMenu> MainMenus { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuDetail> MenuDetails { get; set; }
        public DbSet<SubMenuImageDetail> SubMenuImageDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Error> Errors { get; set; }

    }
}
