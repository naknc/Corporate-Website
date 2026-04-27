using KurumsalWeb.Filters;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using KurumsalWeb.Models.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        CorporateDBContext db = new CorporateDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var model = new AdminDashboardViewModel
            {
                CategoryCount = db.Category.Count(),
                BlogCount = db.Blog.Count(),
                ServiceCount = db.Service.Count(),
                SliderCount = db.Slider.Count(),
                ContactCount = db.Contact.Count(),
                AdminCount = db.Admin.Count(),
                LatestBlogs = db.Blog
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.BlogId)
                    .Take(5)
                    .ToList(),
                LatestServices = db.Service
                    .OrderByDescending(x => x.ServiceId)
                    .Take(5)
                    .ToList()
            };

            return View(model);
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin admin)
        {
            if (admin == null || string.IsNullOrWhiteSpace(admin.Email) || string.IsNullOrWhiteSpace(admin.Password))
            {
                ViewBag.Alert = "Username or password is not right!";
                return View(admin);
            }

            var login = db.Admin.SingleOrDefault(x => x.Email == admin.Email);
            if (login != null && IsPasswordValid(admin.Password, login.Password))
            {
                Session["adminid"] = login.AdminId;
                Session["email"] = login.Email;

                // Upgrade legacy plain-text passwords to hash after successful login.
                if (login.Password == admin.Password)
                {
                    login.Password = Crypto.HashPassword(admin.Password);
                    db.SaveChanges();
                }

                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Alert = "Username or password is not right!";
            return View(admin);
        }
        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["email"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Admin");
        }

        private static bool IsPasswordValid(string inputPassword, string storedPassword)
        {
            if (string.IsNullOrEmpty(storedPassword))
            {
                return false;
            }

            if (storedPassword == inputPassword)
            {
                return true;
            }

            try
            {
                return Crypto.VerifyHashedPassword(storedPassword, inputPassword);
            }
            catch
            {
                return false;
            }
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
