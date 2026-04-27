using KurumsalWeb.Filters;
using KurumsalWeb.Helpers;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    [AdminAuthorize]
    public class SliderController : Controller
    {
        private readonly CorporateDBContext db = new CorporateDBContext();

        public ActionResult Index()
        {
            return View(db.Slider.OrderBy(x => x.SliderId).ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider, HttpPostedFileBase ImageURL)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }

            if (ImageURL != null)
            {
                string imagePath;
                string uploadError;
                if (!ImageUploadHelper.TrySaveResizedImage(Server, ImageURL, "/Uploads/Slider", 1200, 500, out imagePath, out uploadError))
                {
                    ModelState.AddModelError("ImageURL", uploadError);
                    return View(slider);
                }

                slider.ImageURL = imagePath;
            }

            db.Slider.Add(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Slider slider, HttpPostedFileBase ImageURL)
        {
            if (!ModelState.IsValid)
            {
                return View(slider);
            }

            var existingSlider = db.Slider.Find(id);
            if (existingSlider == null)
            {
                return HttpNotFound();
            }

            if (ImageURL != null)
            {
                string imagePath;
                string uploadError;
                if (!ImageUploadHelper.TrySaveResizedImage(Server, ImageURL, "/Uploads/Slider", 1200, 500, out imagePath, out uploadError))
                {
                    ModelState.AddModelError("ImageURL", uploadError);
                    return View(slider);
                }

                if (!string.IsNullOrWhiteSpace(existingSlider.ImageURL) && System.IO.File.Exists(Server.MapPath(existingSlider.ImageURL)))
                {
                    System.IO.File.Delete(Server.MapPath(existingSlider.ImageURL));
                }

                existingSlider.ImageURL = imagePath;
            }

            existingSlider.Title = slider.Title;
            existingSlider.Description = slider.Description;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }

            if (!string.IsNullOrWhiteSpace(slider.ImageURL) && System.IO.File.Exists(Server.MapPath(slider.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath(slider.ImageURL));
            }

            db.Slider.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
