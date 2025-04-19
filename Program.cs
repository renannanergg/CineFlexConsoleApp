using CineFlexConsoleApp.Data;
using CineFlexConsoleApp.Menus;
using CineFlexConsoleApp.Repositories;
using CineFlexConsoleApp.Services;

namespace CineFlexConsoleApp
{
    public class Program
    {
        public static async Task Main()
        {
            CineDataContext context = new();

            // Repositórios
            FilmeRepository filmeRepository = new(context);
            SalaRepository salaRepository = new(context);
            SessaoRepository sessaoRepository = new(context);
            IngressoRepository ingressoRepository = new(context);

            // Serviços
            FilmeService filmeService = new(filmeRepository);
            SalaService salaService = new(salaRepository);
            SessaoService sessaoService = new(sessaoRepository);
            IngressoService ingressoService = new(ingressoRepository);

            // Menus
            MenuFilme menuFilme = new(filmeService);
            MenuSala menuSala = new(salaService);
            MenuSessao menuSessao = new(sessaoService, filmeService, salaService);
            MenuIngresso menuIngresso = new(ingressoService, sessaoService, filmeService);

            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("          MENU PRINCIPAL         ");
            Console.WriteLine("=================================");
            Console.WriteLine("1 - Gerenciar Filmes");
            Console.WriteLine("2 - Gerenciar Salas");
            Console.WriteLine("3 - Gerenciar Sessões");
            Console.WriteLine("4 - Gerenciar Ingressos");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("=================================");
            Console.Write("Escolha uma opção: ");

            string escolha = Console.ReadLine()!;

            switch (escolha)
            {
                case "1":
                    await menuFilme.ExibirMenuAsync();
                    break;
                case "2":
                    await menuSala.ExibirMenuAsync();
                    break;
                case "3":
                    await menuSessao.ExibirMenuAsync();
                    break;
                case "4":
                    await menuIngresso.ExibirMenuAsync();
                    break;
                case "0":
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;

            }
        }
    }
}