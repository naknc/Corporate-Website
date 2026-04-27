using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly CorporateDBContext db = new CorporateDBContext();

        // GET: Home
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;

            var model = new HomePageViewModel
            {
                Identity = db.Identity.FirstOrDefault(),
                AboutUs = db.AboutUs.FirstOrDefault(),
                Contact = db.Contact.FirstOrDefault(),
                Sliders = db.Slider.OrderBy(x => x.SliderId).Take(5).ToList(),
                Services = db.Service.OrderBy(x => x.ServiceId).Take(6).ToList(),
                LatestBlogs = db.Blog
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.BlogId)
                    .Take(6)
                    .ToList()
            };

            return View(model);
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
