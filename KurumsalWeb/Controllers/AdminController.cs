
using KurumsalWeb.Models;
using KurumsalWeb.Models.DataContext;
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
    }
}