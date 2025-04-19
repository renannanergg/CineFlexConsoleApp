using CineFlexConsoleApp.Models;
using CineFlexConsoleApp.Services.Interfaces;

namespace CineFlexConsoleApp.Menus
{
    public class MenuSessao
    {
        private readonly ISessaoService _sessaoService;
        private readonly IFilmeService _filmeService;
        private readonly ISalaService _salaService;

        public MenuSessao(ISessaoService sessaoService,IFilmeService filmeService,
            ISalaService salaService)
        {
            _sessaoService = sessaoService;
            _filmeService = filmeService;
            _salaService = salaService;
        }

        public async Task ExibirMenuAsync()
        {
            Console.Clear();
            Console.WriteLine("----- [Gerenciar Sessões] -----");
            Console.WriteLine();
            Console.WriteLine("[1] Cadastrar Sessão");
            Console.WriteLine("[2] Listar Sessões");
            Console.WriteLine("[3] Buscar Sessão");
            Console.WriteLine("[4] Excluir Sessão");
            Console.WriteLine("[0] Voltar");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Clear();
                    await CadastrarSessao();
                    break;
                case "2":
                    Console.Clear();
                    await ListarSessoes();
                    break;
                case "3":
                    Console.Clear();
                    await BuscarSessaoPorId();
                    break;
                case "4":
                    Console.Clear();
                    await ExcluirSessao();
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

        public async Task ExcluirSessao()
        {
            Console.WriteLine("----- [Excluir Sessão] -----");
            Console.WriteLine();

            Console.Write("ID da Sessão que deseja excluir: ");
            var id = int.Parse(Console.ReadLine()!);

            var sessao = await _sessaoService.BuscarPorIdAsync(id);
            if (sessao != null)
            { 
                await _sessaoService.DeletarSessaoAsync(id);
            }
            else
                Console.WriteLine("Sessão não encontrada.");
        }
        public async Task CadastrarSessao()
        {
            Console.WriteLine("----- [Cadastrar Sessão] -----");
            Console.WriteLine();

            var filmes = await _filmeService.ListarTodosAsync();
            var salas = await _salaService.ListarTodosAsync();

            if (!filmes.Any() || !salas.Any())
            {
                Console.Clear();
                Console.WriteLine("Não há filmes/salas disponíveis.");
                return;
            }

            Console.WriteLine("[Filmes]:");
            foreach (var f in filmes)
                Console.WriteLine($"---- ID: {f.Id} | Título: {f.Titulo}");

            Console.WriteLine();
            Console.Write("Digite o ID do filme: ");
            if (!int.TryParse(Console.ReadLine(), out int filmeId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            var filme = await _filmeService.BuscarPorIdAsync(filmeId);

            Console.WriteLine();
            Console.WriteLine("[Salas]:");
            foreach (var s in salas)
                Console.WriteLine($"----ID: {s.Id} | Numero: {s.NumeroDaSala}");

            Console.Write("Digite o ID da sala: ");
            if (!int.TryParse(Console.ReadLine(), out int salaId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            var sala = await _salaService.BuscarPorIdAsync(salaId);

            Console.WriteLine();
            Console.Write("Digite o data e horário da sessão (ex: 2025-04-12 19:30): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime horario))
            {
                Console.WriteLine("Horário inválido.");
                return;
            }

            var novaSessao = new Sessao
            {
                FilmeId = filmeId,
                Filme = filme,
                SalaId = salaId,
                Horario = horario,
                
            };

            filme.Sessoes.Add(novaSessao);
            sala.Sessoes.Add(novaSessao);

            await _sessaoService.CriarSessaoAsync(novaSessao);
           
        }

        public async Task BuscarSessaoPorId()
        {
            Console.WriteLine("----- [Buscar Sessão] -----");
            Console.WriteLine();

            Console.Write("ID da Sessão que deseja buscar: ");
            var id = int.Parse(Console.ReadLine()!);

            var sessao = await _sessaoService.BuscarPorIdAsync(id);
            if (sessao != null)
            {
                Console.WriteLine($"|Sessão: {sessao.Id}");
                Console.WriteLine($"|Filme: {sessao.Filme.Titulo}");
                Console.WriteLine($"|Horário: {sessao.Horario.ToString("HH:mm")}");
                Console.WriteLine($"|Sala:{sessao.Sala.NumeroDaSala}");
                  
            }
            else
            {
                Console.WriteLine("Sessão não encontrada.");
            }

        }
           
        public async Task ListarSessoes()
        {
            Console.WriteLine("----- [Listar Sessões] -----");
            Console.WriteLine();

            var sessoes = await _sessaoService.ListarTodasAsync();
            if( sessoes.Any())
            {
                foreach (var sessao in sessoes)
                {
                    Console.WriteLine($"|Sessão: {sessao.Id}|Filme: {sessao.Filme.Titulo}|Sala: {sessao.Sala.NumeroDaSala}|Horário: {sessao.Horario.ToString("HH:mm")}");
                }
            }
            else
            {
                Console.WriteLine("Nenhuma sessão encontrada");
            }
        }  

    }
}