using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace KurumsalWeb.Models.DataContext
{
    public class CorporateDBContext: DbContext
    {
        public CorporateDBContext(): base ("CorporateWebDB")
        {

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Model.Service> Service { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Identity> Identity { get; set; }
        public DbSet<Slider> Slider { get; set; }

    }
}