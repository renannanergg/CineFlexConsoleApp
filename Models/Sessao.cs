namespace CineFlexConsoleApp.Models
{
    public class Sessao
    {
        public Sessao(int filmeId, int salaId, DateTime horario, Filme filme, Sala sala)
        {
            FilmeId = filmeId;
            SalaId = salaId;
            Horario = horario;
            Filme = filme;
            Sala = sala;

        }
        public Sessao() { }
        public int Id { get; set; }
        public int FilmeId { get; set; }
        public int SalaId { get; set; }
        public DateTime Horario { get; set; }
        public Filme Filme { get; set; }
        public Sala Sala { get; set; }

        // 1 sessão pode ter vários ingressos
        public IList<Ingresso> Ingressos { get; set; } = new List<Ingresso>();
       

    }
}