namespace ColegioGuaxinim.Domain.Entities
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Mensalidade { get; set; }
        public DateTime DataDeVencimento { get; set; }
        public int ProfessorId { get; set; }
        public Professor Professor { get; set; } = null!;
    }
}
