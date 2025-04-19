using CineFlexConsoleApp.Models;

namespace CineFlexConsoleApp.Services.Interfaces
{
    public interface ISalaService
    {
        Task<Sala> BuscarPorIdAsync(int id);
        Task<IEnumerable<Sala>> ListarTodosAsync();
        Task CadastrarAsync(Sala sala);
        Task AtualizarAsync(Sala sala);
        Task RemoverAsync(int id);

        Task<IEnumerable<Sala>> BuscarPorCapacidadeMinimaAsync(int capacidadeMinima);
    }
}