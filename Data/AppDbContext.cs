using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Mvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TuristickaAgencija.Mvc.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Destinacija> Destinacije { get; set; }
        public DbSet<Aranzman> Aranzmani { get; set; }
    }
}       