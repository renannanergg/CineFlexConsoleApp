using CineFlexConsoleApp.Data;
using CineFlexConsoleApp.Repositories.Interfaces;
using CineFlexConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CineFlexConsoleApp.Repositories
{
    public class SalaRepository : ISalaRepository
    {
        private readonly CineDataContext _context;
        public SalaRepository(CineDataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Sala sala)
        {
            await _context.Salas.AddAsync(sala);
        }

        public async Task DeleteAsync(Sala sala)
        {
            _context.Salas.Remove(sala);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Sala>> GetAllAsync()
        {
            return await _context.Salas.AsNoTracking().ToListAsync();
        }

        public async Task<Sala> GetByIdAsync(int id)
        {
           return await _context.Salas.FindAsync(id);
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

        public async Task UpdateAsync(Sala sala)
        {
            _context.Salas.Update(sala);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Sala>> BuscarPorCapacidadeMinimaAsync(int capacidadeMinima)
        {
            return await _context.Salas
            .Where(s => s.Capacidade >= capacidadeMinima)
            .AsNoTracking()
            .ToListAsync();
        }
    }
}