using CineFlexConsoleApp.Services.Interfaces;
using CineFlexConsoleApp.Models;
using CineFlexConsoleApp.Repositories.Interfaces;

namespace CineFlexConsoleApp.Services
{
    public class SalaService : ISalaService
    {
        private readonly ISalaRepository _salaRepository;
        public SalaService(ISalaRepository salaRepository)
        {
            _salaRepository = salaRepository;
        }
        public async Task AtualizarAsync(Sala sala)
        {
            try
            {
                await _salaRepository.UpdateAsync(sala);
                await _salaRepository.SaveChangesAsync();
                Console.WriteLine("Sala atualizada com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar sala: {ex.Message}");
                
            }
        }

        public async Task<IEnumerable<Sala>> BuscarPorCapacidadeMinimaAsync(int capacidadeMinima)
        {
            try
            {
                return await _salaRepository.BuscarPorCapacidadeMinimaAsync(capacidadeMinima);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar salas: {ex.Message}");
                throw;
            }
        }

        public async Task<Sala> BuscarPorIdAsync(int id)
        {
            try
            {
                return await _salaRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar sala: {ex.Message}");
                throw;
            }
        }

        public async Task CadastrarAsync(Sala sala)
        {
            try
            {
                await _salaRepository.AddAsync(sala);
                await _salaRepository.SaveChangesAsync();
                Console.WriteLine($"Sala cadastrada com sucesso !");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar sala:");
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<IEnumerable<Sala>> ListarTodosAsync()
        {
            try
            {
                return await _salaRepository.GetAllAsync();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar salas: {ex.Message}");
                throw;
            }
        }

        public async Task RemoverAsync(int id)
        {
           try
           {
                var sala = await _salaRepository.GetByIdAsync(id);
                if (sala != null)
                {
                    await _salaRepository.DeleteAsync(sala);
                    await _salaRepository.SaveChangesAsync();
                    Console.WriteLine("Sala removida com sucesso");
                }
                else
                {
                    Console.WriteLine("Sala não encontrada.");
                }
           }
           catch (Exception ex)
           {
                Console.WriteLine($"Erro ao remover sala: {ex.Message}");
           }
        }
    }
}