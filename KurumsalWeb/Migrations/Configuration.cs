namespace KurumsalWeb.Migrations
{
    using KurumsalWeb.Models.Model;
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<KurumsalWeb.Models.DataContext.CorporateDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(KurumsalWeb.Models.DataContext.CorporateDBContext context)
        {
            var adminEmail = ConfigurationManager.AppSettings["AdminSeedEmail"] ?? "admin@local";
            var adminPassword = ConfigurationManager.AppSettings["AdminSeedPassword"] ?? "ChangeMe123!";

            if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
            {
                return;
            }

            context.Admin.AddOrUpdate(
                x => x.Email,
                new Admin
                {
                    Email = adminEmail,
                    Password = Crypto.HashPassword(adminPassword),
                    Authentication = "Admin"
                });
        }
    }
}
