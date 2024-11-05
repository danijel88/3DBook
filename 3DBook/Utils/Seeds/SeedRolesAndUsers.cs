using _3DBook.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace _3DBook.Utils.Seeds;

public  class SeedRolesAndUsers
{
    public  async Task SeedUserRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Administrator"))
        {
            await roleManager.CreateAsync(new IdentityRole("Administrator"));
        }
        if (!await roleManager.RoleExistsAsync("Manager"))
        {
            await roleManager.CreateAsync(new IdentityRole("Manager"));
        }
        if (!await roleManager.RoleExistsAsync("Operator"))
        {
            await roleManager.CreateAsync(new IdentityRole("Operator"));
        }
        if (!await roleManager.RoleExistsAsync("Member"))
        {
            await roleManager.CreateAsync(new IdentityRole("Member"));
        }

        if (await userManager.FindByEmailAsync("admin@company.rs") == null)
        {
            await userManager.CreateAsync(new User
            {
                FirstName = "Admin",
                LastName = "Admin",
                Company = "Company",
                Email = "admin@company.rs",
                UserName = "admin@company.rs"
            },"P@s$w0rd");
            var user = await userManager.FindByEmailAsync("admin@company.rs");
            await userManager.AddToRoleAsync(user!, "Administrator");
        }
    }
}