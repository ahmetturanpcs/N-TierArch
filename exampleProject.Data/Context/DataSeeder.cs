using exampleProject.Core.Consts; // PermissionConsts için
using Microsoft.AspNetCore.Identity;
using System.Security.Claims; // Claim sınıfı için
using System.Threading.Tasks;

namespace exampleProject.Data.Context
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAndUsersAsync(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            // 1. Süper Admin Rolü Yoksa Oluştur
            string roleName = "SuperAdmin";
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                await roleManager.CreateAsync(role);

                // 2. Tüm Yetkileri (PermissionConsts) Bu Role "Claim" Olarak Bağla
                // Kategoriler için yazdığımız tüm yetkileri bu role veriyoruz
                var permissions = new[]
                {
                    PermissionConsts.Category.View,
                    PermissionConsts.Category.Create,
                    PermissionConsts.Category.Update,
                    PermissionConsts.Category.Delete
                };

                foreach (var permission in permissions)
                {
                    // Başına açıkça System.Security.Claims.Claim yazarak netleştiriyoruz
                    await roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("Permission", permission));
                }
            }

            // 3. Test Kullanıcısı Yoksa Oluştur
            string adminEmail = "cemre@example.com";
            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                var newAdmin = new IdentityUser
                {
                    UserName = "cemre",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdmin, "Cemre123!"); // Güvenli şifre
                if (result.Succeeded)
                {
                    // Kullanıcıyı Süper Admin Rolüne Atıyoruz
                    await userManager.AddToRoleAsync(newAdmin, roleName);
                }
            }
        }
    }
}