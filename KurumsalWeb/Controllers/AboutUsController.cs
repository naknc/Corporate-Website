using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class AboutUsController : Controller
    {
        CorporateDBContext db = new CorporateDBContext();
        // GET: AboutUs
        public ActionResult Index()
        {
            var h = db.AboutUs.ToList();
            return View(h);
        }

        public ActionResult Edit(int id)
        {
            var h = db.AboutUs.Where(x => x.AboutUsId == id).FirstOrDefault();
            return View(h);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, AboutUs a)
        {
            if (ModelState.IsValid)
            {
                var aboutus = db.AboutUs.Where(x => x.AboutUsId == id).SingleOrDefault();

                aboutus.Description = a.Description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(a);
        }
    }
}