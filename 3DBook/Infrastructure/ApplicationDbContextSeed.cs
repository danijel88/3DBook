using Microsoft.AspNetCore.Identity;
using System;
using _3DBook.Core;

namespace _3DBook.Infrastructure;

public static class ApplicationDbContextSeed
{
    public static async Task SeedRolesAndDefaultAdminUserAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Administrator"))
        {
            await roleManager.CreateAsync(new IdentityRole("Administrator"));
        }
        if (!await roleManager.RoleExistsAsync("Manager"))
        {
            await roleManager.CreateAsync(new IdentityRole("Manager"));
        }
        if (!await roleManager.RoleExistsAsync("Member"))
        {
            await roleManager.CreateAsync(new IdentityRole("Member"));
        }
        if (!await roleManager.RoleExistsAsync("Operator"))
        {
            await roleManager.CreateAsync(new IdentityRole("Operator"));
        }

        try
        {
            var user = await userManager.FindByEmailAsync(DefaultAdminUser.DEFAULT_EMAIL);
            if (user == null)
            {
                await userManager.CreateAsync(new User
                {
                    FirstName = DefaultAdminUser.DEFAULT_FIRST_NAME,
                    LastName = DefaultAdminUser.DEFAULT_LAST_NAME,
                    Email = DefaultAdminUser.DEFAULT_EMAIL,
                    UserName = DefaultAdminUser.DEFAULT_EMAIL,
                    Company = DefaultAdminUser.DEFAULT_COMPANY,
                    IsActive = true
                }, DefaultAdminUser.DEFAULT_PASSWORD);
                user = await userManager.FindByEmailAsync(DefaultAdminUser.DEFAULT_EMAIL);
                await userManager.AddToRoleAsync(user, "Administrator");
            }
        }
        catch (Exception ex)
        {

        }


    }
    
}