using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniOrderManagement.Models;

namespace MiniOrderManagement.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var provider = scope.ServiceProvider;

            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            var db = provider.GetRequiredService<AppDbContext>();

            // roles
            string[] roles = new[] { "Admin", "User" };
            foreach (var r in roles)
            {
                if (!await roleManager.RoleExistsAsync(r))
                    await roleManager.CreateAsync(new IdentityRole(r));
            }

            // admin user
            var adminEmail = "admin@local";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var res = await userManager.CreateAsync(admin, "Admin@123");
                if (res.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }

            // sample product
            if (!await db.Products.AnyAsync())
            {
                db.Products.Add(new Product { Name = "Sample Product", Price = 100000m, Description = "Demo", Stock = 10 });
                await db.SaveChangesAsync();
            }

            // sample customer (optional)
            if (!await db.Customers.AnyAsync())
            {
                db.Customers.Add(new Customer { Name = "Khách Demo", Email = "customer@local", Phone = "0123456789", Address = "Hanoi" });
                await db.SaveChangesAsync();
            }
        }
    }
}
