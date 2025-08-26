namespace ColegioGuaxinim.Domain.Entities
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime? BloquearTempoDeImportacao { get; set; }
        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
    }
}
