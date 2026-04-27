using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KurumsalWeb.Migrations;
using KurumsalWeb.Models.DataContext;

namespace KurumsalWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CorporateDBContext, Configuration>());
            using (var db = new CorporateDBContext())
            {
                db.Database.Initialize(false);
            }

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
