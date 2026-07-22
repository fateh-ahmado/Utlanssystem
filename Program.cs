using Microsoft.EntityFrameworkCore;
using Utlanssystem.Data;
using Utlanssystem.Models;
using Microsoft.AspNetCore.Identity; // dette er lagt til for å bruke IdentityDbContext

var builder = WebApplication.CreateBuilder(args);

//Dette la vi til for å få tilgang til databasen.
builder.Services.AddDbContext<UtlanssystemContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("UtlanssystemContext")));

// Identity-oppsett - Dette "skrur på" hele innloggingssystemet (registrering, login, passord).
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UtlanssystemContext>();

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
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();