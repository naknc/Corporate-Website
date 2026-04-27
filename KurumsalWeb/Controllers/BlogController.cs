using KurumsalWeb.Filters;
using KurumsalWeb.Helpers;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    [AdminAuthorize]
    public class BlogController : Controller
    {
        private CorporateDBContext db = new CorporateDBContext();

        // GET: Blog

        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return View(db.Blog.Include("Category").ToList().OrderByDescending(x => x.BlogId));
        }
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog, HttpPostedFileBase ImageURL)
        {
            if (ImageURL != null)
            {
                string imagePath;
                string uploadError;
                if (!ImageUploadHelper.TrySaveResizedImage(Server, ImageURL, "/Uploads/Blog", 600, 400, out imagePath, out uploadError))
                {
                    ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", blog.CategoryId);
                    ModelState.AddModelError("ImageURL", uploadError);
                    return View(blog);
                }

                blog.ImageURL = imagePath;
            }
            db.Blog.Add(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var b = db.Blog.Where(x => x.BlogId == id).SingleOrDefault();
            if (b == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", b.CategoryId);
            return View(b);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Blog blog, HttpPostedFileBase ImageURL)
        {
            if (ModelState.IsValid)
            {
                var b = db.Blog.Where(x => x.BlogId == id).SingleOrDefault();
                if (ImageURL != null)
                {
                    string imagePath;
                    string uploadError;
                    if (!ImageUploadHelper.TrySaveResizedImage(Server, ImageURL, "/Uploads/Blog", 600, 400, out imagePath, out uploadError))
                    {
                        ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "CategoryName", blog.CategoryId);
                        ModelState.AddModelError("ImageURL", uploadError);
                        return View(blog);
                    }

                    if (!string.IsNullOrWhiteSpace(b.ImageURL) && System.IO.File.Exists(Server.MapPath(b.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(b.ImageURL));
                    }

                    b.ImageURL = imagePath;
                }
                b.Title = blog.Title;
                b.Contents = blog.Contents;
                b.CategoryId = blog.CategoryId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        public ActionResult Delete(int id)
        {
            var b = db.Blog.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            if (!string.IsNullOrWhiteSpace(b.ImageURL) && System.IO.File.Exists(Server.MapPath(b.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath(b.ImageURL));
            }
            db.Blog.Remove(b);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
