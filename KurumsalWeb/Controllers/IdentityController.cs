using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
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
                    if (System.IO.File.Exists(Server.MapPath(iden.LogoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(iden.LogoURL));
                    }
                    WebImage img = new WebImage(LogoURL.InputStream);
                    FileInfo imginfo = new FileInfo(LogoURL.FileName);

                    string logoname = LogoURL.FileName + imginfo.Extension;
                    img.Resize(300, 200);
                    img.Save("~/Uploads/Identity/"+logoname);

                    iden.LogoURL = "/Uploads/Identity/" + logoname;
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
