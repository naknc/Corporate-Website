using KurumsalWeb.Filters;
using KurumsalWeb.Helpers;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    [AdminAuthorize]
    public class ServiceController : Controller
    {
        private CorporateDBContext db = new CorporateDBContext();
        // GET: Service
        public ActionResult Index()
        {
            return View(db.Service.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service, HttpPostedFileBase ImageURL)
        {
            if (ModelState.IsValid)
            {
                if (ImageURL != null)
                {
                    string imagePath;
                    string uploadError;
                    if (!ImageUploadHelper.TrySaveResizedImage(Server, ImageURL, "/Uploads/Service", 500, 500, out imagePath, out uploadError))
                    {
                        ModelState.AddModelError("ImageURL", uploadError);
                        return View(service);
                    }

                    service.ImageURL = imagePath;
                }
                db.Service.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Alert = "There is no Service to update!";
            }
            var service = db.Service.Find(id);
            if (service == null)
            {
                return HttpNotFound();

            }
            return View(service);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, Service service, HttpPostedFileBase ImageURL)
        {
            if (ModelState.IsValid)
            {
                var s = db.Service.Where(x => x.ServiceId == id).SingleOrDefault();
                if (ImageURL != null)
                {
                    string imagePath;
                    string uploadError;
                    if (!ImageUploadHelper.TrySaveResizedImage(Server, ImageURL, "/Uploads/Service", 500, 500, out imagePath, out uploadError))
                    {
                        ModelState.AddModelError("ImageURL", uploadError);
                        return View(service);
                    }

                    if (!string.IsNullOrWhiteSpace(s.ImageURL) && System.IO.File.Exists(Server.MapPath(s.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(s.ImageURL));
                    }
                    s.ImageURL = imagePath;

                }

                s.Title = service.Title;
                s.Description = service.Description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            var s = db.Service.Find(id);
            if (s==null)
            {
                return HttpNotFound();
            }
            db.Service.Remove(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
