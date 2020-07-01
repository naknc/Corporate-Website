
using KurumsalWeb.Models;
using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        CorporateDBContext db = new CorporateDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            var sorgu = db.Category.ToList();
            return View(sorgu);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login = db.Admin.Where(x => x.Email == admin.Email).SingleOrDefault();
            if (login.Email==admin.Email && login.Password==admin.Password)
            {
                Session["adminid"] = login.AdminId;
                Session["email"] = login.Email;
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
    }
}