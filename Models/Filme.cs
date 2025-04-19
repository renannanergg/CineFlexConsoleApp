namespace CineFlexConsoleApp.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Duracao { get; set; }  // em minutos
        public string Genero { get; set; }

        // Relacionamento: 1 filme pode ter várias sessões
        public IList<Sessao> Sessoes { get; set; } = new List<Sessao>();
    }

    
}