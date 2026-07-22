/*
Vi byttet til IdentityDbContext<IdentityUser> fordi vi trengte innlogging og roller (Admin)
i appen. IdentityDbContext er en FERDIG versjon av DbContext, laget av Microsoft, som allerede
inneholder tabeller for brukere og roller (AspNetUsers, AspNetRoles, AspNetUserRoles, m.fl.)
*/
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Utlanssystem.Models;

namespace Utlanssystem.Data
{
    // public class UtlanssystemContext : DbContext
    public class UtlanssystemContext : IdentityDbContext<IdentityUser>
    {
        public UtlanssystemContext(DbContextOptions<UtlanssystemContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Loan> Loans { get; set; }

         // + AspNetUsers, AspNetRoles osv. - kommer GRATIS fra IdentityDbContext, usynlig i koden
    }
}