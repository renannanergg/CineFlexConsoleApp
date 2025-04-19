using CineFlexConsoleApp.Data;
using CineFlexConsoleApp.Models;
using CineFlexConsoleApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CineFlexConsoleApp.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly CineDataContext _context;
        public FilmeRepository(CineDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Filme filme)
        {
            await _context.Filmes.AddAsync(filme);
        }

        public async Task<IEnumerable<Filme>> BuscarPorGeneroAsync(string genero)
        {
            return await _context.Filmes
                .AsNoTracking()
                .Where(f => f.Genero.Contains(genero))
                .ToListAsync();
        }
        public async Task<IEnumerable<Filme>> BuscarComSessoesAsync()
        {
            return await _context.Filmes
            .Where(f => f.Sessoes.Any()) // Filme com pelo menos uma sessão
            .Include(f => f.Sessoes)     // Inclui as sessões, se desejar exibir
            .AsNoTracking()
            .ToListAsync();
        }
        public async Task<IEnumerable<Filme>> BuscarPorTituloAsync(string tituloParcial)
        {
            return await _context.Filmes
            .Where(f => f.Titulo.Contains(tituloParcial))
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task DeleteAsync(Filme filme)
        {
            _context.Filmes.Remove(filme);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Filme>> GetAllAsync()
        {
            return await _context.Filmes.AsNoTracking().ToListAsync();
        }

        public async Task<Filme> GetByIdAsync(int id)
        {
            return await _context.Filmes.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine(" Erro ao salvar dados no banco:");
                Console.WriteLine(dbEx.InnerException?.Message ?? dbEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                // Qualquer outro erro inesperado
                Console.WriteLine(" Erro inesperado ao salvar alterações:");
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(Filme filme)
        {
            _context.Filmes.Update(filme);
            await Task.CompletedTask;
        }
    }
}