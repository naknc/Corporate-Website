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
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(KurumsalWeb.Models.DataContext.CorporateDBContext context)
        {
            var adminEmail = ConfigurationManager.AppSettings["AdminSeedEmail"];
            var adminPassword = ConfigurationManager.AppSettings["AdminSeedPassword"];

            // Seed admin only if explicit values are provided.
            var canSeedAdmin = !string.IsNullOrWhiteSpace(adminEmail) &&
                               !string.IsNullOrWhiteSpace(adminPassword) &&
                               adminEmail != "__SET_ADMIN_EMAIL__" &&
                               adminPassword != "__SET_ADMIN_PASSWORD__";

            if (canSeedAdmin)
            {
                context.Admin.AddOrUpdate(
                    x => x.Email,
                    new Admin
                    {
                        Email = adminEmail,
                        Password = Crypto.HashPassword(adminPassword),
                        Authentication = "Admin"
                    });
            }

            context.Identity.AddOrUpdate(
                x => x.Title,
                new Identity
                {
                    Title = "Corporate Website",
                    Keywords = "corporate, consulting, strategy, technology",
                    Description = "Professional services and corporate updates.",
                    LogoURL = "/Content/AdminLayout/img/logo-big.png",
                    Degree = "24"
                });

            var about = context.AboutUs.FirstOrDefault();
            if (about == null)
            {
                context.AboutUs.Add(new AboutUs
                {
                    Description = "<p>We help organizations design better operations, modernize technology foundations, and build measurable growth plans.</p><p>Our multidisciplinary team combines strategy, design, and engineering to deliver reliable outcomes.</p>"
                });
            }
            else
            {
                about.Description = "<p>We help organizations design better operations, modernize technology foundations, and build measurable growth plans.</p><p>Our multidisciplinary team combines strategy, design, and engineering to deliver reliable outcomes.</p>";
            }

            var contact = context.Contact.FirstOrDefault();
            if (contact == null)
            {
                context.Contact.Add(new Contact
                {
                    Address = "Maslak Mah. Büyükdere Cad. No:101, Sarıyer / Istanbul",
                    Phone = "+90 212 555 01 01",
                    Fax = "+90 212 555 01 02",
                    Whatsapp = "+90 532 000 00 00",
                    Facebook = "https://facebook.com/corporatewebsite",
                    Twitter = "https://x.com/corporatewebsite",
                    Instagram = "https://instagram.com/corporatewebsite"
                });
            }
            else
            {
                contact.Address = "Maslak Mah. Büyükdere Cad. No:101, Sarıyer / Istanbul";
                contact.Phone = "+90 212 555 01 01";
                contact.Fax = "+90 212 555 01 02";
                contact.Whatsapp = "+90 532 000 00 00";
                contact.Facebook = "https://facebook.com/corporatewebsite";
                contact.Twitter = "https://x.com/corporatewebsite";
                contact.Instagram = "https://instagram.com/corporatewebsite";
            }

            context.Category.AddOrUpdate(
                x => x.CategoryName,
                new Category { CategoryName = "Strategy", Description = "Growth and transformation strategy insights." },
                new Category { CategoryName = "Technology", Description = "Modern software and platform guidance." },
                new Category { CategoryName = "Operations", Description = "Operational excellence and process design." });
            context.SaveChanges();

            context.Service.AddOrUpdate(
                x => x.Title,
                new Service
                {
                    Title = "Digital Transformation",
                    Description = "End-to-end transformation roadmap, portfolio prioritization, and execution support.",
                    ImageURL = "/Content/AdminLayout/img/sample-img-1.jpg"
                },
                new Service
                {
                    Title = "Process Optimization",
                    Description = "Operational process redesign with measurable efficiency and quality targets.",
                    ImageURL = "/Content/AdminLayout/img/sample-img-2.png"
                },
                new Service
                {
                    Title = "Data & Analytics",
                    Description = "Decision dashboards, KPI modeling, and analytics operating model setup.",
                    ImageURL = "/Content/AdminLayout/img/sample-img-3.png"
                },
                new Service
                {
                    Title = "Product Strategy",
                    Description = "Product positioning, value proposition design, and release planning.",
                    ImageURL = "/Content/AdminLayout/img/sample-img-1.jpg"
                },
                new Service
                {
                    Title = "Cloud Enablement",
                    Description = "Cloud migration planning, cost optimization, and governance controls.",
                    ImageURL = "/Content/AdminLayout/img/sample-img-2.png"
                },
                new Service
                {
                    Title = "Change Management",
                    Description = "Training, communication, and adoption programs for sustainable change.",
                    ImageURL = "/Content/AdminLayout/img/sample-img-3.png"
                });

            context.Slider.AddOrUpdate(
                x => x.Title,
                new Slider
                {
                    Title = "Trusted Partner",
                    Description = "Helping teams deliver change with confidence.",
                    ImageURL = "/Content/AdminLayout/img/sample-img-1.jpg"
                },
                new Slider
                {
                    Title = "Scalable Solutions",
                    Description = "Architecture and operations designed for growth.",
                    ImageURL = "/Content/AdminLayout/img/sample-img-2.png"
                },
                new Slider
                {
                    Title = "Outcome Focus",
                    Description = "Clear KPIs and tangible business impact.",
                    ImageURL = "/Content/AdminLayout/img/sample-img-3.png"
                });
            context.SaveChanges();

            var strategyId = context.Category.Where(x => x.CategoryName == "Strategy").Select(x => x.CategoryId).FirstOrDefault();
            var technologyId = context.Category.Where(x => x.CategoryName == "Technology").Select(x => x.CategoryId).FirstOrDefault();
            var operationsId = context.Category.Where(x => x.CategoryName == "Operations").Select(x => x.CategoryId).FirstOrDefault();

            context.Blog.AddOrUpdate(
                x => x.Title,
                new Blog
                {
                    Title = "Building a Practical 12-Month Transformation Plan",
                    Contents = "<p>A pragmatic transformation plan starts with measurable business targets, cross-functional ownership, and realistic sequencing.</p><p>Start small, prove value fast, and scale with strong governance.</p>",
                    ImageURL = "/Content/AdminLayout/img/sample-img-1.jpg",
                    CategoryId = strategyId
                },
                new Blog
                {
                    Title = "Modernizing Legacy Systems Without Service Disruption",
                    Contents = "<p>Legacy modernization works best when teams decouple risks and move in controlled increments.</p><p>Use an architecture runway and quality gates to preserve stability.</p>",
                    ImageURL = "/Content/AdminLayout/img/sample-img-2.png",
                    CategoryId = technologyId
                },
                new Blog
                {
                    Title = "Operational KPIs That Actually Drive Behavior",
                    Contents = "<p>Good KPIs are actionable, attributable, and tied to decision cadences.</p><p>Teams should understand how daily actions impact weekly outcomes.</p>",
                    ImageURL = "/Content/AdminLayout/img/sample-img-3.png",
                    CategoryId = operationsId
                },
                new Blog
                {
                    Title = "Product Discovery for B2B Teams",
                    Contents = "<p>Discovery is not a one-time phase. It is a continuous loop of hypothesis, evidence, and iteration.</p><p>Align discovery outputs directly with roadmap decisions.</p>",
                    ImageURL = "/Content/AdminLayout/img/sample-img-1.jpg",
                    CategoryId = strategyId
                },
                new Blog
                {
                    Title = "Cloud Cost Optimization in 5 Practical Steps",
                    Contents = "<p>Cost optimization requires visibility, ownership, and architectural hygiene.</p><p>Rightsizing, storage lifecycle rules, and workload scheduling create immediate gains.</p>",
                    ImageURL = "/Content/AdminLayout/img/sample-img-2.png",
                    CategoryId = technologyId
                },
                new Blog
                {
                    Title = "Leading Change Across Distributed Teams",
                    Contents = "<p>Distributed teams need predictable rituals, transparent communication, and local champions.</p><p>Adoption metrics should be monitored as closely as delivery metrics.</p>",
                    ImageURL = "/Content/AdminLayout/img/sample-img-3.png",
                    CategoryId = operationsId
                });
        }
    }
}
