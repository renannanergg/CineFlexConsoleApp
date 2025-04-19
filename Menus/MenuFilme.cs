using CineFlexConsoleApp.Models;
using CineFlexConsoleApp.Services.Interfaces;

namespace CineFlexConsoleApp.Menus
{
    public class MenuFilme
    {
        private readonly IFilmeService _filmeService;
        public MenuFilme(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        public async Task ExibirMenuAsync()
        {
            Console.Clear();
            Console.WriteLine("----- [Gerenciar Filmes] -----");
            Console.WriteLine();
            Console.WriteLine("[1] Cadastrar Filme");
            Console.WriteLine("[2] Listar Filmes");
            Console.WriteLine("[3] Buscar Filme pelo ID");
            Console.WriteLine("[4] Buscar Filme pelo Título");
            Console.WriteLine("[5] Buscar Filmes pelo Gênero");
            Console.WriteLine("[6] Buscar Filmes com Sessões");
            Console.WriteLine("[7] Atualizar Filme");
            Console.WriteLine("[8] Excluir Filme");
            Console.WriteLine("[0] Voltar ");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Clear();
                    await CadastrarFilmeAsync();
                    break;
                case "2":
                    Console.Clear();
                    await ListarFilmesAsync();
                    break;
                case "3":
                    Console.Clear();
                    await BuscarPorUmFilmePeloId();
                    break;
                case "4":
                    Console.Clear();
                    await BuscarFilmePorTitulo();
                    break;
                case "5":
                    Console.Clear();
                    await BuscarFilmesPorGenero();
                    break;
                case "6":
                    Console.Clear();
                    await ListarFilmesComSessoes();
                    break;
                case "7":
                    Console.Clear();
                    await AtualizarFilme();
                    break;
                case "8":
                    Console.Clear();
                    await DeletarFilme();
                    break;
                case "0":
                    Console.Clear();
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
                    
            }

        }

        private async Task DeletarFilme()
        {
            Console.WriteLine("----- [Excluir Filme] -----");
            Console.WriteLine();

            Console.Write("ID do Filme que deseja excluir: ");
            var id = int.Parse(Console.ReadLine()!);
            
            Console.WriteLine();
            await _filmeService.RemoverAsync(id);
        }

        private async Task AtualizarFilme()
        {
            Console.WriteLine("----- [Atualizar Filme] -----");
            Console.WriteLine();

            Console.Write("ID do Filme que deseja atualizar: ");
            var id = int.Parse(Console.ReadLine()!);
            
            var filmeAtualizado = await _filmeService.BuscarPorIdAsync(id);
            Console.WriteLine();
            if (filmeAtualizado != null)
            {
                Console.Write("Título do filme: ");
                var titulo = Console.ReadLine()!;

                Console.Write("Gênero: ");
                var genero = Console.ReadLine()!;

                Console.Write("Duração (min): ");
                var duracao = int.Parse(Console.ReadLine()!);

                filmeAtualizado.Titulo = titulo;
                filmeAtualizado.Genero = genero;
                filmeAtualizado.Duracao = duracao;

                await _filmeService.AtualizarAsync(filmeAtualizado);
            }
            else
            {
                Console.WriteLine("Nenhum filme encontrado");
            }

        }

        private async Task ListarFilmesComSessoes()
        {
            Console.WriteLine("----- [Lista de Filmes] -----");
            Console.WriteLine();
            
            var filmes = await _filmeService.ListarFilmesComSessoesAsync();
            if (filmes.Any())
            {
                foreach (var filme in filmes)
                {
                    Console.WriteLine($"|Filme: {filme.Titulo} |Sessões: {filme.Sessoes.Count}");
                    foreach (var sessao in filme.Sessoes)
                    {
                        Console.WriteLine($"----- Sala: {sessao.Sala.NumeroDaSala}/{sessao.Horario.ToString("HH:mm")}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Nenhum filme com sessão encontrado");
            }
        }

        private async Task BuscarFilmesPorGenero()
        {
            Console.WriteLine("----- [Buscar Filmes] -----");
            Console.WriteLine();

            Console.Write("Gênero: ");
            var genero = Console.ReadLine()!;

            var filmes = await _filmeService.BuscarPorGeneroAsync(genero);
            Console.WriteLine();
            if (filmes.Any())
            {
                foreach (var filme in filmes)
                {
                    Console.WriteLine($"|Título: {filme.Titulo} |Gênero: {filme.Genero} |Sessões: {filme.Sessoes.Count}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum filme encontrado.");
            }
        }

        private async Task BuscarFilmePorTitulo()
        {
            Console.WriteLine("----- [Buscar Filme] -----");
            Console.WriteLine();

            Console.Write("Título do filme: ");
            var titulo = Console.ReadLine()!;

            var filmes = await _filmeService.BuscarPorTituloAsync(titulo);
            Console.WriteLine();
            if (filmes.Any())
            {
                foreach (var filme in filmes)
                {
                    Console.WriteLine($"|Título: {filme.Titulo} |Gênero: {filme.Genero} |Sessões: {filme.Sessoes.Count}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum filme encontrado.");
            }
        }

        private async Task BuscarPorUmFilmePeloId()
        {
            Console.WriteLine("----- [Buscar Filme] -----");
            Console.WriteLine();

            Console.Write("ID do filme que deseja buscar: ");
            var id = int.Parse(Console.ReadLine()!);

            var filme = await _filmeService.BuscarPorIdAsync(id);
            if (filme != null)
            {
                Console.WriteLine();
                Console.WriteLine($"|Título: {filme.Titulo}");
                Console.WriteLine($"|Duração: {filme.Duracao} min");
                Console.WriteLine($"|Gênero: {filme.Genero}");
                Console.WriteLine($"|Sessões disponíveis: {filme.Sessoes.Count}");
                Console.WriteLine("---- Horários disponíveis: ");
                foreach (var sessao in filme.Sessoes)
                {
                    Console.WriteLine($"--{sessao.Horario.ToString("HH:mm")}");
                }
            }
            else
            {
                Console.WriteLine("Filme não encontrado.");
            }
        }

        public async Task CadastrarFilmeAsync()
        {
            Console.WriteLine("----- [Cadastrar Filme] -----");
            Console.WriteLine();
            
            Console.Write("Título do filme: ");
            var titulo = Console.ReadLine()!;

            Console.Write("Gênero: ");
            var genero = Console.ReadLine()!;

            Console.Write("Duração (min): ");
            var duracao = int.Parse(Console.ReadLine()!);

            var novoFilme = new Filme
            {
                Titulo = titulo,
                Genero = genero,
                Duracao = duracao,
            };

            await _filmeService.CadastrarAsync(novoFilme);
        }

        public async Task ListarFilmesAsync()
        {
            Console.WriteLine("----- [Lista de Filmes] -----");
            Console.WriteLine();
            var filmes = await _filmeService.ListarTodosAsync();
            if (filmes.Any())
            {
                foreach (var filme in filmes)
                {
                    Console.WriteLine($"|Filme: {filme.Titulo} |Gênero {filme.Genero} |Duração (min): {filme.Duracao} |Sessões: {filme.Sessoes.Count}");
                    foreach (var sessao in filme.Sessoes)
                    {
                        Console.WriteLine($"------{sessao.Sala.NumeroDaSala} | {sessao.Horario.ToString("HH:mm")}");
                    }
                }
            }
            else
            {
                System.Console.WriteLine("Nenhum filme encontrado.");
            }
        }
    }
}