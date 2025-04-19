using CineFlexConsoleApp.Models;

namespace CineFlexConsoleApp.Services.Interfaces
{
    public interface IIngressoService
    {
        Task BuscarPorIdAsync(int id);
        Task ListarPorSessaoAsync(int sessaoId);
        Task<bool> AssentoDisponivelAsync(int sessaoId, string assento);
        Task ComprarIngressoAsync(Ingresso ingresso);
        Task CancelarIngressoAsync(int id);
        Task AtualizarIngressoAsync(Ingresso ingresso);
    }
}