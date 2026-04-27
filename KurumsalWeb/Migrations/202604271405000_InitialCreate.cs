namespace KurumsalWeb.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutUs",
                c => new
                    {
                        AboutUsId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AboutUsId);

            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 255),
                        Authentication = c.String(),
                    })
                .PrimaryKey(t => t.AdminId);

            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);

            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        BlogId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Contents = c.String(),
                        ImageURL = c.String(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.BlogId)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .Index(t => t.CategoryId);

            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        Address = c.String(maxLength: 250),
                        Phone = c.String(maxLength: 250),
                        Fax = c.String(),
                        Whatsapp = c.String(),
                        Facebook = c.String(),
                        Twitter = c.String(),
                        Instagram = c.String(),
                    })
                .PrimaryKey(t => t.ContactId);

            CreateTable(
                "dbo.Identity",
                c => new
                    {
                        IdentityId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Keywords = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 300),
                        LogoURL = c.String(),
                        Degree = c.String(),
                    })
                .PrimaryKey(t => t.IdentityId);

            CreateTable(
                "dbo.Service",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        ImageURL = c.String(),
                    })
                .PrimaryKey(t => t.ServiceId);

            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        SliderId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 30),
                        Description = c.String(maxLength: 150),
                        ImageURL = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.SliderId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Blog", "CategoryId", "dbo.Category");
            DropIndex("dbo.Blog", new[] { "CategoryId" });
            DropTable("dbo.Slider");
            DropTable("dbo.Service");
            DropTable("dbo.Identity");
            DropTable("dbo.Contact");
            DropTable("dbo.Blog");
            DropTable("dbo.Category");
            DropTable("dbo.Admin");
            DropTable("dbo.AboutUs");
        }
    }
}
