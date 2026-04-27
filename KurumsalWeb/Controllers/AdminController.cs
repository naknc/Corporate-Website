using KurumsalWeb.Filters;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using KurumsalWeb.Models.ViewModel;
using System.Configuration;
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
            EnsureSeedAdminAccount();
            ViewBag.Alert = TempData["Alert"];
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [SafeValidateAntiForgeryToken]
        public ActionResult Login(Admin admin)
        {
            EnsureSeedAdminAccount();

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

        private void EnsureSeedAdminAccount()
        {
            var seedEmail = ConfigurationManager.AppSettings["AdminSeedEmail"];
            var seedPassword = ConfigurationManager.AppSettings["AdminSeedPassword"];

            if (string.IsNullOrWhiteSpace(seedEmail) ||
                string.IsNullOrWhiteSpace(seedPassword) ||
                seedEmail == "__SET_ADMIN_EMAIL__" ||
                seedPassword == "__SET_ADMIN_PASSWORD__")
            {
                return;
            }

            var admin = db.Admin.SingleOrDefault(x => x.Email == seedEmail);
            if (admin == null)
            {
                db.Admin.Add(new Admin
                {
                    Email = seedEmail,
                    Password = Crypto.HashPassword(seedPassword),
                    Authentication = "Admin"
                });
                db.SaveChanges();
                return;
            }

            if (!IsPasswordValid(seedPassword, admin.Password))
            {
                admin.Password = Crypto.HashPassword(seedPassword);
                if (string.IsNullOrWhiteSpace(admin.Authentication))
                {
                    admin.Authentication = "Admin";
                }
                db.SaveChanges();
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
