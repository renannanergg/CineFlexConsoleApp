using CineFlexConsoleApp.Models;
using CineFlexConsoleApp.Services.Interfaces;

namespace CineFlexConsoleApp.Menus
{
    public class MenuSala
    {
        private readonly ISalaService _salaService;

        public MenuSala(ISalaService salaService)
        {
            _salaService = salaService;
        }

        public async Task ExibirMenuAsync()
        {
            Console.Clear();
            Console.WriteLine("----- [Gerenciar Salas] -----");
            Console.WriteLine();
            Console.WriteLine("[1] Cadastrar Sala");
            Console.WriteLine("[2] Listar Salas");
            Console.WriteLine("[3] Buscar Sala");
            Console.WriteLine("[4] Buscar Salas por Capacidade Mínima");
            Console.WriteLine("[5] Atualizar Sala");
            Console.WriteLine("[6] Excluir Sala");
            Console.WriteLine("[0] Voltar");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.Clear();
                    await CadastrarSala();
                    break;
                case "2":
                    Console.Clear();
                    await ListarTodasSalas();
                    break;
                case "3":
                    Console.Clear();
                    await BuscarSalaPorId();
                    break;
                case "4":
                    Console.Clear();
                    await BuscarSalasPorCapacidadeMinima();
                    break;
                case "5":
                    Console.Clear();
                    await AtualizarSala();
                    break;
                case "6":
                    Console.Clear();
                    await DeletarSala();
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

        private async Task DeletarSala()
        {
            Console.WriteLine("----- [Excluir Sala] -----");
            Console.WriteLine();

            Console.Write("ID da Sala que deseja excluir: ");
            var id = int.Parse(Console.ReadLine()!);

            Console.WriteLine();
            await _salaService.RemoverAsync(id);
        }

        private async Task AtualizarSala()
        {
            Console.WriteLine("----- [Atualizar Sala] -----");
            Console.WriteLine();

            Console.Write("ID da Sala que deseja atualizar: ");
            var id = int.Parse(Console.ReadLine()!);

            var salaAtualizada = await _salaService.BuscarPorIdAsync(id);
            if (salaAtualizada != null)
            {
                Console.Write("Numero da Sala: ");
                var numeroSala = int.Parse(Console.ReadLine()!);

                Console.Write("Capacidade: ");
                var capacidade = int.Parse(Console.ReadLine()!);

                salaAtualizada.NumeroDaSala = numeroSala;
                salaAtualizada.Capacidade = capacidade;

                await _salaService.AtualizarAsync(salaAtualizada);
            }
            else
            {
                Console.WriteLine("Nenhuma sala encontrada.");
            }
            
        }

        private async Task BuscarSalasPorCapacidadeMinima()
        {
            Console.WriteLine("----- [Listar Salas] -----");
            Console.WriteLine();

            Console.Write("Digite a Capacidade Mínima: ");
            var capacidadeMinima = int.Parse(Console.ReadLine()!);

            var salas = await _salaService.BuscarPorCapacidadeMinimaAsync(capacidadeMinima);
            Console.WriteLine();
            if (salas.Any())
            {
                foreach (var sala in salas)
                {
                    Console.WriteLine($"|Sala: {sala.NumeroDaSala} |Sessões: {sala.Sessoes.Count} ");
                }
            }
            else
            {
                Console.WriteLine("Nenhuma sala encontrada.");
            }
        }

        private async Task BuscarSalaPorId()
        {
            Console.WriteLine("----- [Buscar Sala] -----");
            Console.WriteLine();

            Console.Write("ID da Sala que deseja buscar: ");
            var id = int.Parse(Console.ReadLine()!);

            var sala = await _salaService.BuscarPorIdAsync(id);
            Console.WriteLine();
            if (sala != null)
            {
                Console.WriteLine($"|Sala:{sala.NumeroDaSala}");
                Console.WriteLine($"|Sessões disponíveis: {sala.Sessoes.Count}");
                Console.WriteLine($"");
                foreach (var sessao in sala.Sessoes)
                {
                    Console.WriteLine($"--|{sessao.Filme.Titulo} |{sessao.Horario.ToString("HH:mm")}");
                }
            }
            else
            {
                Console.WriteLine("Sala não encontrada.");
            }
        }

        private async Task ListarTodasSalas()
        {
            Console.WriteLine("----- [Listar Salas] -----");
            Console.WriteLine();

            var salas = await _salaService.ListarTodosAsync();
            Console.WriteLine();
            if (salas.Any())
            {
                foreach (var sala in salas)
                {
                    Console.WriteLine($"|Sala: {sala.NumeroDaSala} |Capacidade: {sala.Capacidade} |Sessões: {sala.Sessoes.Count} ");
                }
            }
            else
            {
                Console.WriteLine("Nenhuma sala encontrada.");
            }
        }
        private async Task CadastrarSala()
        {
            Console.WriteLine("----- [Cadastrar Sala] -----");
            Console.WriteLine();

            Console.Write("Numero da Sala: ");
            var numeroSala = int.Parse(Console.ReadLine()!);

            Console.Write("Capacidade: ");
            var capacidade = int.Parse(Console.ReadLine()!);

            var sala = new Sala()
            {
                NumeroDaSala = numeroSala,
                Capacidade = capacidade
            };

            await _salaService.CadastrarAsync(sala);
        }
    }
}