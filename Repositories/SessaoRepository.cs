using CineFlexConsoleApp.Data;
using CineFlexConsoleApp.Repositories.Interfaces;
using CineFlexConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CineFlexConsoleApp.Repositories
{
    public class SessaoRepository : ISessaoRepository
    {
        private readonly CineDataContext _context;
        public SessaoRepository(CineDataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Sessao sessao)
        {
            await _context.Sessoes.AddAsync(sessao);
        }

        public async Task DeleteAsync(Sessao sessao)
        {
            _context.Sessoes.Remove(sessao);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Sessao>> GetAllAsync()
        {
            return await _context.Sessoes
            .Include(s => s.Filme)
            .Include(s => s.Sala)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Sessao> GetByIdAsync(int id)
        {
           return await _context.Sessoes
            .Include(s => s.Filme)
            .Include(s => s.Sala)
            .FirstOrDefaultAsync(s => s.Id == id);
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

        public async Task UpdateAsync(Sessao sessao)
        {
            _context.Sessoes.Update(sessao);
            await Task.CompletedTask;
        }

        // Método customizado: 
        public async Task<IEnumerable<Sessao>> BuscarPorFilmeAsync(int filmeId)
        {
            return await _context.Sessoes
            .Where(s => s.FilmeId == filmeId && s.Horario > DateTime.Now)
            .Include(s => s.Sala)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<IEnumerable<Sessao>> BuscarPorNomeFilmeAsync(string nomeFilme)
        {
            return await _context.Sessoes
                .Include(s => s.Filme)
                .Include(s => s.Sala)
                .Where(s => s.Filme.Titulo.Contains(nomeFilme))
                .ToListAsync();
        }
    }
}