using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Mvc.Data;
using TuristickaAgencija.Mvc.Models;

namespace TuristickaAgencija.Mvc.Services
{
    public class DestinacijaService : IDestinacijaService
    {
        private readonly AppDbContext _context;

        public DestinacijaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Destinacija>> GetAllAsync()
        {
            return await _context.Destinacije.ToListAsync();
        }

        public async Task<Destinacija?> GetByIdAsync(int id)
        {
            return await _context.Destinacije.FindAsync(id);
        }

        public async Task CreateAsync(Destinacija destinacija)
        {
            _context.Destinacije.Add(destinacija);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Destinacija destinacija)
        {
            _context.Entry(destinacija).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var destinacija = await _context.Destinacije.FindAsync(id);
            if (destinacija != null)
            {
                _context.Destinacije.Remove(destinacija);
                await _context.SaveChangesAsync();
            }
        }

        public bool Exists(int id)
        {
            return _context.Destinacije.Any(e => e.Id == id);
        }

        public async Task<List<Destinacija>> GetPopularneAsync()
        {
            return await _context.Destinacije
                .Where(d => d.Popularna)
                .OrderBy(d => d.Naziv)
                .ToListAsync();
        }

        public async Task<List<Destinacija>> GetPoDrzaviAsync(string drzava)
        {
            return await _context.Destinacije
                .Where(d => d.Drzava.Contains(drzava))
                .OrderBy(d => d.Naziv)
                .ToListAsync();
        }
    }
}