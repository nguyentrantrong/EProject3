using Eproject3.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Lab>();
builder.Services.AddScoped<Supplier>();
builder.Services.AddDbContext<eProject3Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectDb")));
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = Eproject3.Controllers.SchemesNamesConst.TokenAuthenticationDefaultScheme;
    o.DefaultScheme = Eproject3.Controllers.SchemesNameConst.TokenAuthenticationDefaultScheme;
});
builder.Services.AddSession();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("StaffOnly", policy => policy.RequireRole("Staff"));
    options.AddPolicy("UserOnly",  policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
<<<<<<< HEAD
    pattern: "{controller=Account}/{action=Login}/{id?}");
=======
    pattern: "{controller=Devices}/{action=Index}/{id?}");
>>>>>>> main

app.Run();
