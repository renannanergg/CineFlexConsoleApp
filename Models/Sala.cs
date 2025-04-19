namespace CineFlexConsoleApp.Models
{
    public class Sala
    {
        public int Id { get; set; }
        public int NumeroDaSala { get; set; } // Ex: Sala 10
        public int Capacidade { get; set; }
        
        // Relacionamento: 1 sala pode ter várias sessões
        public ICollection<Sessao> Sessoes { get; set; } = new List<Sessao>();

    }
}