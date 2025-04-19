using CineFlexConsoleApp.Data;
using CineFlexConsoleApp.Repositories.Interfaces;
using CineFlexConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CineFlexConsoleApp.Repositories
{
    public class IngressoRepository : IIngressoRepository
    {
        private readonly CineDataContext _context;

        public IngressoRepository(CineDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Ingresso ingresso)
        {
            await _context.Ingressos.AddAsync(ingresso);
        }

        public async Task DeleteAsync(Ingresso ingresso)
        {
            _context.Ingressos.Remove(ingresso);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Ingresso>> GetAllAsync()
        {
            return await _context.Ingressos.AsNoTracking().ToListAsync();
        }

        public async Task<Ingresso> GetByIdAsync(int id)
        {
           try
            {
                return await _context.Ingressos
                                    .Include(i => i.Sessao)  // Se o ingresso tiver a sessão carregada
                                    .ThenInclude(s => s.Filme)  // Se a sessão tiver o filme carregado
                                    .FirstOrDefaultAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar ingresso no banco: {ex.Message}");
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine(" Erro ao salvar no banco:");
                Console.WriteLine(dbEx.InnerException?.Message ?? dbEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Erro inesperado:");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(Ingresso ingresso)
        {
            _context.Ingressos.Update(ingresso);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Ingresso>> BuscarPorSessaoAsync(int sessaoId)
        {
            return await _context.Ingressos
            .Where(i => i.SessaoId == sessaoId)
            .ToListAsync();
        }

        public async Task<bool> AssentoDisponivelAsync(int sessaoId, string assento)
        {
            return !await _context.Ingressos
            .AnyAsync(i => i.SessaoId == sessaoId && i.Assento == assento);
            // true = assento está disponível, false = já ocupado
        }

        public async Task<IEnumerable<Ingresso>> ListarPorSessaoAsync(int sessaoId)
        {
            return await _context.Ingressos
                .Where(i => i.SessaoId == sessaoId)
                .AsNoTracking()
                .ToListAsync();
        }

       
    }
}