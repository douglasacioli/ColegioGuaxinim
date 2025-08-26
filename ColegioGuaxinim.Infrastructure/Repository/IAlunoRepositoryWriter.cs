using ColegioGuaxinim.Domain.Entities;

namespace ColegioGuaxinim.Infrastructure.Repository
{
    public interface IAlunoRepositoryWriter
    {
        Task AdicionarVariosAsync(IEnumerable<Aluno> alunos, CancellationToken ct = default);
        Task RemoverAlunoAsync(Aluno aluno, CancellationToken ct = default);
        Task SalvarAlteracoesAsync(CancellationToken ct = default);
    }
}
