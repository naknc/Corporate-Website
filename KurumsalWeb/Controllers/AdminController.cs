
using KurumsalWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        CorporateDB db = new CorporateDB();
        // GET: Admin
        public ActionResult Index()
        {
            var sorgu = db.Categories.ToList();
            return View(sorgu);
        }
    }
}