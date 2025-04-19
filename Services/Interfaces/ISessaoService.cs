using CineFlexConsoleApp.Models;

namespace CineFlexConsoleApp.Services.Interfaces
{
    public interface ISessaoService
    {
        Task<IEnumerable<Sessao>> ListarTodasAsync();
        Task<Sessao> BuscarPorIdAsync(int id);
        Task<IEnumerable<Sessao>> BuscarPorFilmeAsync(int filmeId);
        Task CriarSessaoAsync(Sessao sessao);
        Task AtualizarSessaoAsync(Sessao sessao);
        Task DeletarSessaoAsync(int id);
        Task<IEnumerable<Sessao>> BuscarPorNomeFilmeAsync(string nomeFilme);
    }
}