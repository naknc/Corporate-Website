Corporate website project with an admin panel using ASP.NET MVC 5 and Entity Framework 6.

## Quick Setup

1. Update `KurumsalWeb/Web.config` connection string (`CorporateWebDB`) for your SQL Server.
2. Set initial admin credentials with:
   - `AdminSeedEmail`
   - `AdminSeedPassword`
3. Restore NuGet packages.
4. Run EF migration from Package Manager Console:
   - `Update-Database`
