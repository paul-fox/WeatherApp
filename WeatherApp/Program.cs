using Microsoft.EntityFrameworkCore;
using WeatherApp.Context;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MySettingsModel>(
    builder.Configuration.GetSection("MySettings"));

builder.Services.AddTransient<ILocationApiService, LocationApiService>();
builder.Services.AddTransient<IWeatherApiService, WeatherApiService>();
builder.Services.AddTransient<IWeatherSqlService, WeatherSqlService>();

builder.Services.AddHostedService<WeatherBackgroundService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

//Service for DB Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
