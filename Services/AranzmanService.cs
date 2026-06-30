using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Mvc.Data;
using TuristickaAgencija.Mvc.Models;

namespace TuristickaAgencija.Mvc.Services
{
    public class AranzmanService : IAranzmanService
    {
        private readonly AppDbContext _context;

        public AranzmanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Aranzman>> GetAllAsync()
        {
            return await _context.Aranzmani.ToListAsync();
        }

        public async Task<Aranzman?> GetByIdAsync(int id)
        {
            return await _context.Aranzmani.FindAsync(id);
        }

        public async Task CreateAsync(Aranzman aranzman)
        {
            _context.Aranzmani.Add(aranzman);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Aranzman aranzman)
        {
            _context.Entry(aranzman).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var aranzman = await _context.Aranzmani.FindAsync(id);
            if (aranzman != null)
            {
                _context.Aranzmani.Remove(aranzman);
                await _context.SaveChangesAsync();
            }
        }

        public bool Exists(int id)
        {
            return _context.Aranzmani.Any(e => e.Id == id);
        }

        public async Task<List<Aranzman>> GetByMaxCijenaAsync(decimal maxCijena)
        {
            return await _context.Aranzmani
                .Include(a => a.Destinacija)
                .Where(a => a.Cijena <= maxCijena)
                .ToListAsync();
        }

        public async Task<List<Aranzman>> GetNadolazeciAsync(decimal maxCijena)
        {
            var today = DateTime.Today;
            return await _context.Aranzmani
                .Include(a => a.Destinacija)
                .Where(a => a.Cijena <= maxCijena)
                .OrderBy(a => a.DatumPolaska)
                .ToListAsync();
        }

        public async Task<List<Aranzman>> GetNadolazeciAsync()
        {
            return await _context.Aranzmani
                .Include(a => a.Destinacija)
                .Where(a => a.DatumPolaska >= DateTime.Today)
                .OrderBy(a => a.DatumPolaska)
                .ToListAsync();
        }

        public async Task<List<Aranzman>> GetAranzmanByPriceAsc()
        {
            return await _context.Aranzmani
                .OrderBy(a => a.Cijena)
                .ToListAsync(); //usmeni
        }
    }
}