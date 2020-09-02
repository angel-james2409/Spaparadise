using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SpaParadise.Models
{
    public class Context : DbContext
    {

        public Context() : base("cs") { }

        public DbSet<Admin> admins { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Booking> bookings { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<Cart> carts { get; set; }

    }
}
