using KurumsalWeb.Filters;
using KurumsalWeb.Helpers;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    [AdminAuthorize]
    public class IdentityController : Controller
    {
        CorporateDBContext db = new CorporateDBContext();
        // GET: Identity
        public ActionResult Index()
        {
            return View(db.Identity.ToList());
        }

        // GET: Identity/Edit/5
        public ActionResult Edit(int id)
        {
            var identity = db.Identity.Where(x => x.IdentityId == id).SingleOrDefault();
            return View(identity);
        }

        // POST: Identity/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Identity identity, HttpPostedFileBase LogoURL)
        {
            if (ModelState.IsValid)
            {
                var iden = db.Identity.Where(x => x.IdentityId == id).SingleOrDefault();

                if (LogoURL != null)
                {
                    string imagePath;
                    string uploadError;
                    if (!ImageUploadHelper.TrySaveResizedImage(Server, LogoURL, "/Uploads/Identity", 300, 200, out imagePath, out uploadError))
                    {
                        ModelState.AddModelError("LogoURL", uploadError);
                        return View(identity);
                    }

                    if (!string.IsNullOrWhiteSpace(iden.LogoURL) && System.IO.File.Exists(Server.MapPath(iden.LogoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(iden.LogoURL));
                    }

                    iden.LogoURL = imagePath;
                }
                iden.Title = identity.Title;
                iden.Keywords = identity.Keywords;
                iden.Description = identity.Description;
                iden.Degree = identity.Degree;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(identity);
        }
    }
}
