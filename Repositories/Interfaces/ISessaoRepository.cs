using CineFlexConsoleApp.Models;

namespace CineFlexConsoleApp.Repositories.Interfaces
{
    public interface ISessaoRepository
    {
        Task<Sessao> GetByIdAsync(int id);
        Task<IEnumerable<Sessao>> GetAllAsync();
        Task AddAsync(Sessao sessao);
        Task UpdateAsync(Sessao sessao);
        Task DeleteAsync(Sessao sessao);
        Task SaveChangesAsync();

        // Método customizado: buscar sessões futuras de um filme
        Task<IEnumerable<Sessao>> BuscarPorFilmeAsync(int filmeId);
        Task<IEnumerable<Sessao>> BuscarPorNomeFilmeAsync(string nomeFilme);
    }
}