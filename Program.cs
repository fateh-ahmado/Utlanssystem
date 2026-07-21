using Microsoft.EntityFrameworkCore;
using Utlanssystem.Data;
using Utlanssystem.Models;

var builder = WebApplication.CreateBuilder(args);
//Dette la vi til for å få tilgang til databasen.
builder.Services.AddDbContext<UtlanssystemContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("UtlanssystemContext")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Dette er koden som kjører SeedData.Initialize() for å fylle databasen med testdata.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
