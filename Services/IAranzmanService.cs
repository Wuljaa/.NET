using TuristickaAgencija.Mvc.Models;

namespace TuristickaAgencija.Mvc.Services
{
    public interface IAranzmanService
    {
        Task<List<Aranzman>> GetAllAsync();
        Task<Aranzman?> GetByIdAsync(int id);
        Task CreateAsync(Aranzman aranzman);
        Task UpdateAsync(Aranzman aranzman);
        Task DeleteAsync(int id);
        bool Exists(int id);
        Task<List<Aranzman>> GetByMaxCijenaAsync(decimal maxCijena);
        Task<List<Aranzman>> GetNadolazeciAsync();
    }

}