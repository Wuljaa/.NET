using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Mvc.Models;

namespace TuristickaAgencija.Mvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Destinacija> Destinacije { get; set; }
        public DbSet<Aranzman> Aranzmani { get; set; }
    }
}       