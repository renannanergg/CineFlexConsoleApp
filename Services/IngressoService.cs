using CineFlexConsoleApp.Services.Interfaces;
using CineFlexConsoleApp.Models;
using CineFlexConsoleApp.Repositories.Interfaces;

namespace CineFlexConsoleApp.Services
{
    public class IngressoService : IIngressoService
    {
        private readonly IIngressoRepository _ingressoRepository;
        public IngressoService(IIngressoRepository ingressoRepository)
        {
            _ingressoRepository = ingressoRepository;
        }
        public async Task<bool> AssentoDisponivelAsync(int sessaoId, string assento)
        {
            return await _ingressoRepository.AssentoDisponivelAsync(sessaoId, assento);
        }

        public async Task AtualizarIngressoAsync(Ingresso ingresso)
        {
            try
            {
                await _ingressoRepository.UpdateAsync(ingresso);
                await _ingressoRepository.SaveChangesAsync();
                Console.WriteLine("Ingresso atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar ingresso: {ex.Message}");
               
            }
        }

        public async Task BuscarPorIdAsync(int id)
        {
            try
            {
                var ingresso = await _ingressoRepository.GetByIdAsync(id);
                if (ingresso != null)
                {
                    Console.WriteLine($"Sessão: {ingresso.Sessao.Id}| {ingresso.Sessao.Filme.Titulo}");
                    Console.WriteLine($"--Sala:{ingresso.Sessao.Sala.NumeroDaSala}");
                    Console.WriteLine($"--Assento:{ingresso.Assento}");
                    Console.WriteLine($"--Horário: {ingresso.Sessao.Horario.ToString("HH:mm")}"); 
                    Console.WriteLine($"--Preço: {ingresso.Preco}");
                }
                else
                {
                    Console.WriteLine("Ingresso não encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar ingresso: {ex.Message}");
                throw;
            }
        }

        public async Task CancelarIngressoAsync(int id)
        {
            try
            {
                var ingresso = await _ingressoRepository.GetByIdAsync(id);
                if (ingresso != null)
                {
                    await _ingressoRepository.DeleteAsync(ingresso);
                    await _ingressoRepository.SaveChangesAsync();
                    Console.WriteLine("Ingresso cancelado com sucesso.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao cancelar ingresso: {ex.Message}");
            }
        }

        public async Task ComprarIngressoAsync(Ingresso ingresso)
        {
           try
           {
                bool disponivel = await _ingressoRepository.AssentoDisponivelAsync(ingresso.SessaoId, ingresso.Assento);
                if (!disponivel)
                {
                    Console.WriteLine("Assento já está ocupado.");
                }
                else
                {
                    await _ingressoRepository.AddAsync(ingresso);
                    await _ingressoRepository.SaveChangesAsync();
                    Console.WriteLine("Ingresso comprado com sucesso.");
                }   
           }
           catch (Exception ex)
           {
                Console.WriteLine($"Erro ao comprar ingresso: {ex.Message}");
           }
        }

        public async Task ListarPorSessaoAsync(int sessaoId)
        {
           try
           {
                var ingressos = await _ingressoRepository.BuscarPorSessaoAsync(sessaoId);
                if (ingressos.Any())
                {
                    foreach (var ingresso in ingressos)
                    {
                        Console.WriteLine($"Assento: {ingresso.Assento}| Preço: {ingresso.Preco}");
                    }
                }
                else
                {
                    Console.WriteLine("Não há ingressos nessa sessão");
                }
           }
           catch (Exception ex)
           {
                Console.WriteLine($"Erro ao listar ingressos: {ex.Message}");
           }
        }

       
    }
}