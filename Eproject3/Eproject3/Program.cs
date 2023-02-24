using Eproject3.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Lab>();
builder.Services.AddScoped<Supplier>();
builder.Services.AddDbContext<eProject3Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Devices}/{action=Index}/{id?}");

app.Run();
