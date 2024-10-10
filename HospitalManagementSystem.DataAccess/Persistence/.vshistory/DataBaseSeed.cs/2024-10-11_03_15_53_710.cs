using HospitalManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace HospitalManagementSystem.DataAccess.Persistence
{
    public static class DataBaseSeed
    {
        private static async Task SeedDatabaseAsync(ApplicationDbContext context,
                                                    UserManager<ApplicationUser> userManager,
                                                    RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = SD.Admin,
                    NormalizedName = SD.Admin.ToUpper(),
                });
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = SD.Admin,
                    NormalizedName = SD.Admin.ToUpper(),
                });
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = SD.Nurse,
                    NormalizedName = SD.Nurse.ToUpper(),
                });
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = SD.SuperAdmin,
                    NormalizedName = SD.SuperAdmin.ToUpper(),
                });
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = SD.Doctor,
                    NormalizedName = SD.Patient.ToUpper(),
                });
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = SD.Patient,
                    NormalizedName = SD.Patient.ToUpper(),
                });

                var admin = new ApplicationUser { UserName = "admin", Email = "admin@admin.com", EmailConfirmed = true };
                await userManager.CreateAsync(admin, "Admin123.?");

                var doctor = new ApplicationUser { UserName = "doctor", Email = "Doctor@admin.com", EmailConfirmed = true };
                await userManager.CreateAsync(doctor, "Admin123.?");

                await userManager.AddToRoleAsync(admin, SD.Admin);
                await userManager.AddToRoleAsync(doctor, SD.Doctor);


            }

            await context.SaveChangesAsync();
        }
        public static async Task SeedDatabaseAsync(IServiceProvider services)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();

            if (context.Database.IsSqlServer()) await context.Database.MigrateAsync();

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await SeedDatabaseAsync(context, userManager, roleManager);
        }

    }
}
