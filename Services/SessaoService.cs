using CineFlexConsoleApp.Services.Interfaces;
using CineFlexConsoleApp.Models;
using CineFlexConsoleApp.Repositories.Interfaces;

namespace CineFlexConsoleApp.Services
{
    public class SessaoService : ISessaoService
    {
        private readonly ISessaoRepository _sessaoRepository;
        public SessaoService(ISessaoRepository sessaoRepository)
        {
            _sessaoRepository = sessaoRepository;
        }
        public async Task AtualizarSessaoAsync(Sessao sessao)
        {
           try
           {
                await _sessaoRepository.UpdateAsync(sessao);
                await _sessaoRepository.SaveChangesAsync();
                Console.WriteLine("Sessão atualizada com sucesso.");
           }
           catch (Exception ex)
           {
                Console.WriteLine($"Erro ao atualizar sessão: {ex.Message}");
           }
        }

        public async Task<IEnumerable<Sessao>> BuscarPorFilmeAsync(int filmeId)
        {
            try
            {
                return await _sessaoRepository.BuscarPorFilmeAsync(filmeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar sessões: {ex.Message}");
                throw;
            }
        }

        public async Task<Sessao> BuscarPorIdAsync(int id)
        {
            try
            {
                return await _sessaoRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar sessãp: {ex.Message}");
                throw;
            }
        }

        public async Task CriarSessaoAsync(Sessao sessao)
        {
            try
            {
                await _sessaoRepository.AddAsync(sessao);
                await _sessaoRepository.SaveChangesAsync();
                Console.WriteLine($"Sessão cadastrada com sucesso !");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar sessão:");
                Console.WriteLine(ex.Message);
                
            }
        }

        public async Task DeletarSessaoAsync(int id)
        {
           try
           {
                var sessao = await _sessaoRepository.GetByIdAsync(id);
                if (sessao != null)
                {
                    await _sessaoRepository.DeleteAsync(sessao);
                    await _sessaoRepository.SaveChangesAsync();
                    Console.WriteLine("Sessão removida com sucesso.");

                }
                else
                {
                    Console.WriteLine("Sessão não encontrada.");
                }
           }
           catch (Exception ex)
           {
                Console.WriteLine($"Erro ao remover sessão: {ex.Message}");
           }
        }

        public async Task<IEnumerable<Sessao>> ListarTodasAsync()
        {
            try
            {
                return await _sessaoRepository.GetAllAsync();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar sessões: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Sessao>> BuscarPorNomeFilmeAsync(string nomeFilme)
        {
            try
            {
                return await _sessaoRepository.BuscarPorNomeFilmeAsync(nomeFilme);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar sessão do filme: {ex.Message}");
                throw;
            }
        }
    }
}