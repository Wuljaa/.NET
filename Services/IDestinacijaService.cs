using TuristickaAgencija.Mvc.Models;

namespace TuristickaAgencija.Mvc.Services
{
    public interface IDestinacijaService
    {
        Task<List<Destinacija>> GetAllAsync();
        Task<Destinacija?> GetByIdAsync(int id);
        Task CreateAsync(Destinacija destinacija);
        Task UpdateAsync(Destinacija destinacija);
        Task DeleteAsync(int id);
        Task<List<Destinacija>>GetPopularneAsync();
        Task<List<Destinacija>> GetPoDrzaviAsync(string drzava);
        bool Exists(int id);
    }
}