using CineFlexConsoleApp.Services.Interfaces;
using CineFlexConsoleApp.Models;
using CineFlexConsoleApp.Repositories.Interfaces;


namespace CineFlexConsoleApp.Services
{
    public class FilmeService : IFilmeService
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmeService(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public async Task CadastrarAsync(Filme filme)
        {
            try
            {
                await _filmeRepository.AddAsync(filme);
                await _filmeRepository.SaveChangesAsync();
                Console.WriteLine($"Filme {filme.Titulo} cadastrado com sucesso !");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao cadastrar filme:");
                Console.WriteLine(ex.Message);
            }
        }

        public async Task<IEnumerable<Filme>> ListarTodosAsync()
        {
            try
            {
                return await _filmeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Erro ao listar filmes: {ex.Message}");
               throw;
            }
        }

        public async Task<Filme> BuscarPorIdAsync(int id)
        {
            try
            {
                var filme = await _filmeRepository.GetByIdAsync(id);
                return filme;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar filme: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Filme>> BuscarPorTituloAsync(string titulo)
        {
            try
            {
                return await _filmeRepository.BuscarPorTituloAsync(titulo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar filmes: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Filme>> BuscarPorGeneroAsync(string genero)
        {
            try
            {
                return await _filmeRepository.BuscarPorGeneroAsync(genero);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar filmes: {ex.Message}");
                throw;
            }
        }

        public async Task AtualizarAsync(Filme filme)
        {
            try
            {
                await _filmeRepository.UpdateAsync(filme);
                await _filmeRepository.SaveChangesAsync();
                Console.WriteLine("Filme atualizado com sucesso.");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar filme: {ex.Message}");

            }
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var filme = await _filmeRepository.GetByIdAsync(id);
                if (filme != null)
                {
                    await _filmeRepository.DeleteAsync(filme);
                    await _filmeRepository.SaveChangesAsync();
                    Console.WriteLine("Filme removido com sucesso.");
                    
                }
                else
                {
                    Console.WriteLine("Filme não encontrado.");
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao remover filme: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Filme>> ListarFilmesComSessoesAsync()
        {
            try
            {
                return await _filmeRepository.BuscarComSessoesAsync();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar filmes com sessões: {ex.Message}");
                throw;
            }
        }
    }
}