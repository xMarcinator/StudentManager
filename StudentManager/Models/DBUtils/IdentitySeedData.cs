using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace StudentManager.Models.DBUtils;

public static class IdentitySeedData
{
    private const string adminUser = "Admin";
    private const string adminPassword = "Secret123$";

    public static async void EnsurePopulated(IApplicationBuilder app)
    {
        AppIdentityDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<AppIdentityDbContext>();
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
        }

        var userManager = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<UserManager<IdentityUser>>();
        
        var roleManager = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        if (!await roleManager.RoleExistsAsync ("Admin"))
        {
            var adminRole = new IdentityRole("Admin");
            await roleManager.CreateAsync (adminRole);
            
            var foreverTrialClaim = new Claim ("Trial", DateTime.Now.AddYears(1).ToString(CultureInfo.CurrentCulture));
            await roleManager.AddClaimAsync (adminRole, foreverTrialClaim);
        }
        
        var user = await userManager.FindByNameAsync(adminUser);
        if (user == null)
        {
            user = new IdentityUser("Admin");
            user.Email = "marcjensenvirklund@gmail.com";
            user.PhoneNumber = "555-1234";
            
            await userManager.CreateAsync(user, adminPassword);
        }
        
        Claim trialClaim = new Claim ("Trial", DateTime.Now.ToString ());
        await userManager.AddClaimAsync (user, trialClaim);
    }
}
