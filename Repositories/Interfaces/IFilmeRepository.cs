using CineFlexConsoleApp.Models;

namespace CineFlexConsoleApp.Repositories.Interfaces
{
    public interface IFilmeRepository
    {
        Task<Filme> GetByIdAsync(int id);
        Task<IEnumerable<Filme>> GetAllAsync();
        Task AddAsync(Filme filme);
        Task UpdateAsync(Filme filme);
        Task DeleteAsync(Filme filme);
        Task SaveChangesAsync();

        // Métodos customizados: 
        Task<IEnumerable<Filme>> BuscarPorTituloAsync(string tituloParcial);
        Task<IEnumerable<Filme>> BuscarPorGeneroAsync(string genero);
        Task<IEnumerable<Filme>> BuscarComSessoesAsync();
    }
}