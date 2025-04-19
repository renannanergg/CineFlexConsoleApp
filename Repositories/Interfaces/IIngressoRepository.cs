using CineFlexConsoleApp.Models;

namespace CineFlexConsoleApp.Repositories.Interfaces
{
    public interface IIngressoRepository
    {
        Task<Ingresso> GetByIdAsync(int id);
        Task<IEnumerable<Ingresso>> GetAllAsync();
        Task AddAsync(Ingresso ingresso);
        Task UpdateAsync(Ingresso ingresso);
        Task DeleteAsync(Ingresso ingresso);
        Task SaveChangesAsync();

        // Métodos customizados: 
        Task<IEnumerable<Ingresso>> BuscarPorSessaoAsync(int sessaoId);
        Task<bool> AssentoDisponivelAsync(int sessaoId, string assento);
        Task<IEnumerable<Ingresso>> ListarPorSessaoAsync(int sessaoId);
        
    }
}