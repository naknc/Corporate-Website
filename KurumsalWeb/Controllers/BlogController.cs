using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class BlogController : Controller
    {
        private CorporateDBContext db = new CorporateDBContext();

        // GET: Blog
        
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Blog.Include("Category").ToList().OrderByDescending(x=>x.BlogId));
        }
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog, HttpPostedFileBase ImageURL)
        {
            if (ImageURL != null)
            {
                WebImage img = new WebImage(ImageURL.InputStream);
                FileInfo imginfo = new FileInfo(ImageURL.FileName);

                string blogimgname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Resize(600, 400);
                img.Save("~/Uploads/Blog/" + blogimgname);

                blog.ImageURL = "/Uploads/Blog/" + blogimgname;
            }
            db.Blog.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}