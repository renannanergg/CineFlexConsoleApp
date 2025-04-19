namespace CineFlexConsoleApp.Models
{
    public class Ingresso
    {
        public int Id { get; set; }
        public int SessaoId { get; set; }
        public string Assento { get; set; } // Exemplo: "B12"
        public decimal Preco { get; set; }
        
        // Relacionamento
        public Sessao Sessao { get; set; }
    }
}