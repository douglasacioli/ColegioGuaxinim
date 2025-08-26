using ColegioGuaxinim.Domain.Entities;

namespace ColegioGuaxinim.Infrastructure.Repository
{
    public interface IAlunoRepository
    {
        Task<List<Aluno>> ListarPorProfessorAsync(int professorId, CancellationToken ct);
        Task<Aluno?> ObterAlunoPorIdAsync(int id, int professorId, CancellationToken ct);
        Task AdicionarVariosAsync(IEnumerable<Aluno> alunos, CancellationToken ct = default);
        Task<bool> RemoverAlunoAsync(int id, CancellationToken ct = default);
        Task SalvarAlteracoesAsync(CancellationToken ct = default);
        Task<Aluno> AtualizarAsync(Aluno aluno, CancellationToken ct = default);
    }
}
