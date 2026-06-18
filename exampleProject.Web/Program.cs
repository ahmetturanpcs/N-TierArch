using exampleProject.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using exampleProject.Data.Context;
using exampleProject.Web.Authorization;
using exampleProject.Core.Repositories;
using exampleProject.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// DbContext Kaydı
builder.Services.AddDbContext<exampleProject.Data.Context.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 1. Standart Identity Servis Kaydı (Kullanıcı ve Rol yönetimi için)
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied"; // Yetkisi yetmeyenlerin gideceği sayfa
});

// 2. Bizim yazdığımız Dinamik Yetki Mekanizmalarının Kaydı
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(exampleProject.Data.Repositories.GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // Kimlik Doğrulama (Ben kimim?)
app.UseAuthorization();  // Yetkilendirme (Neleri yapmaya yetkim var?)

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// Veritabanını ilk verilerle besleme (Seed Data)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    await exampleProject.Data.Context.DataSeeder.SeedRolesAndUsersAsync(roleManager, userManager);
}

app.Run();
