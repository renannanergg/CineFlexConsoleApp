using CineFlexConsoleApp.Models;
using CineFlexConsoleApp.Services.Interfaces;

namespace CineFlexConsoleApp.Menus
{
    public class MenuIngresso
    {
        private readonly IIngressoService _ingressoService;
        private readonly ISessaoService _sessaoService;
        private readonly IFilmeService _filmeService;

        public MenuIngresso(IIngressoService ingressoService,ISessaoService sessaoService, IFilmeService filmeService )
        {
            _ingressoService = ingressoService;
            _sessaoService = sessaoService;
            _filmeService = filmeService;
        }

        public async Task ExibirMenuAsync()
        {
            Console.Clear();
            Console.WriteLine("----- [Gerenciar Ingressos] -----");
            Console.WriteLine();
            Console.WriteLine("[1] Comprar Ingresso");
            Console.WriteLine("[2] Listar Ingressos de uma Sessão");
            Console.WriteLine("[3] Cancelar Ingresso");
            Console.WriteLine("[0] Voltar");
            Console.WriteLine();
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch(opcao)
            {
                case "1":
                    Console.Clear();
                    await CadastrarIngresso();
                    break;
                case "2":
                    Console.Clear();
                    await ListarIngressosPorSessao();
                    break;
                case "3":
                    Console.Clear();
                    await CancelarIngresso();
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

        private async Task CancelarIngresso()
        {
            Console.WriteLine("----- [Cancelar Ingresso] -----");
            Console.WriteLine();

            Console.Write("Digite o ID do ingresso que deseja cancelar: ");
    
            if (!int.TryParse(Console.ReadLine(), out int ingressoId))
            {
                Console.WriteLine("ID inválido. Operação cancelada.");
                return;
            }

            await _ingressoService.CancelarIngressoAsync(ingressoId);
           
        }

        private async Task ListarIngressosPorSessao()
        {
            Console.Write("Digite o ID da sessão para listar os ingressos: ");

            if (!int.TryParse(Console.ReadLine(), out int sessaoId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            Console.WriteLine("Ingressos vendidos para esta sessão:");

            await _ingressoService.ListarPorSessaoAsync(sessaoId);
        }

        private async Task CadastrarIngresso()
        {
            Console.WriteLine("----- [Comprar Ingresso] -----");
            Console.WriteLine();

            Console.Write("Digite o nome do filme: ");
            string nomeFilme = Console.ReadLine()!;

            var sessoes = await _sessaoService.BuscarPorNomeFilmeAsync(nomeFilme);

            if (sessoes == null || !sessoes.Any())
            {
                Console.WriteLine("Nenhuma sessão encontrada para esse filme.");
                return;
            }

            Console.WriteLine("\nSessões disponíveis:");
            foreach (var sessao in sessoes)
            {
                Console.WriteLine($"ID: {sessao.Id} | Sala: {sessao.Sala.NumeroDaSala} | Horário: {sessao.Horario:HH:mm}");
            }

            Console.Write("\nDigite o ID da sessão desejada: ");
            if (!int.TryParse(Console.ReadLine(), out int sessaoId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var sessaoSelecionada = sessoes.FirstOrDefault(s => s.Id == sessaoId);

            if (sessaoSelecionada == null)
            {
                Console.WriteLine("Sessão não encontrada.");
                return;
            }

            Console.Write("Assento desejado (ex: A1): ");
            string assento = Console.ReadLine()!;

            bool disponivel = await _ingressoService.AssentoDisponivelAsync(sessaoId, assento);
            if (!disponivel)
            {
                Console.WriteLine("Assento já está ocupado.");
                return;
            }

            Console.Write("Preço: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal preco))
            {
                Console.WriteLine("Preço inválido.");
                return;
            }

            var ingresso = new Ingresso
            {
                SessaoId = sessaoId,
                Assento = assento,
                Preco = preco
            };

            sessaoSelecionada.Ingressos.Add(ingresso);
            await _ingressoService.ComprarIngressoAsync(ingresso);

        }
    }   

}