using CineFlexConsoleApp.Models;

namespace CineFlexConsoleApp.Repositories.Interfaces
{
    public interface ISalaRepository
    {
        Task<Sala> GetByIdAsync(int id);
        Task<IEnumerable<Sala>> GetAllAsync();
        Task AddAsync(Sala sala);
        Task UpdateAsync(Sala sala);
        Task DeleteAsync(Sala sala);
        Task SaveChangesAsync();

        // método customizado: 
        Task<IEnumerable<Sala>> BuscarPorCapacidadeMinimaAsync(int capacidadeMinima);
    }
}