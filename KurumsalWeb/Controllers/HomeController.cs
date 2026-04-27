using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
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

        private Identity GetSiteIdentity()
        {
            return db.Identity.FirstOrDefault();
        }

        // GET: Home
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var identity = GetSiteIdentity();
            ViewBag.SiteIdentity = identity;

            var model = new HomePageViewModel
            {
                Identity = identity,
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

        public ActionResult Blog()
        {
            db.Configuration.LazyLoadingEnabled = false;
            ViewBag.SiteIdentity = GetSiteIdentity();
            var posts = db.Blog
                .Include(x => x.Category)
                .OrderByDescending(x => x.BlogId)
                .ToList();
            return View(posts);
        }

        public ActionResult BlogDetail(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            ViewBag.SiteIdentity = GetSiteIdentity();
            var post = db.Blog
                .Include(x => x.Category)
                .SingleOrDefault(x => x.BlogId == id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return View(post);
        }

        public ActionResult About()
        {
            db.Configuration.LazyLoadingEnabled = false;
            ViewBag.SiteIdentity = GetSiteIdentity();
            var about = db.AboutUs.FirstOrDefault();
            return View(about);
        }

        public ActionResult Services()
        {
            db.Configuration.LazyLoadingEnabled = false;
            ViewBag.SiteIdentity = GetSiteIdentity();
            var services = db.Service.OrderBy(x => x.ServiceId).ToList();
            return View(services);
        }

        public ActionResult Contact()
        {
            db.Configuration.LazyLoadingEnabled = false;
            ViewBag.SiteIdentity = GetSiteIdentity();
            var contact = db.Contact.FirstOrDefault();
            return View(contact);
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
