using CineFlexConsoleApp.Models;

namespace CineFlexConsoleApp.Services.Interfaces
{
    public interface IFilmeService
    {
        // Método para listar todos os filmes
        Task<IEnumerable<Filme>> ListarTodosAsync();

        // Método para buscar um filme pelo seu ID
        Task<Filme> BuscarPorIdAsync(int id);

        // Método para buscar filmes por título
        Task<IEnumerable<Filme>> BuscarPorTituloAsync(string titulo);

        // Método para buscar filmes por gênero
        Task<IEnumerable<Filme>> BuscarPorGeneroAsync(string genero);

        // Método para cadastrar um novo filme
        Task CadastrarAsync(Filme filme);

        // Método para atualizar informações de um filme
        Task AtualizarAsync(Filme filme);

        // Método para remover um filme pelo ID
        Task RemoverAsync(int id);

        // Método para listar filmes que têm sessões associadas
        Task<IEnumerable<Filme>> ListarFilmesComSessoesAsync();
    }
}