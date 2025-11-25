
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace WebPersonagens.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string AdminRole = "Admin";
            const string PadraoRole = "Padrao";
            const string AdminEmail = "hevellynda.car@gmail.com"; 

            
            if (await roleManager.FindByNameAsync(AdminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(AdminRole));
            }

            if (await roleManager.FindByNameAsync(PadraoRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(PadraoRole));
            }

            var adminUser = await userManager.FindByEmailAsync(AdminEmail);

            if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, AdminRole))
            {
                await userManager.AddToRoleAsync(adminUser, AdminRole);
            }
        }
    }
}