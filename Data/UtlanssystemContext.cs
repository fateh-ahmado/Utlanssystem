using Microsoft.EntityFrameworkCore;
using Utlanssystem.Models;

namespace Utlanssystem.Data
{
    public class UtlanssystemContext : DbContext
    {
        public UtlanssystemContext(DbContextOptions<UtlanssystemContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}