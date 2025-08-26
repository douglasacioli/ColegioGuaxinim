using ColegioGuaxinim.Domain.Entities;

namespace ColegioGuaxinim.Infrastructure.Repository
{
    public interface IAlunoRepositoryReader
    {
        Task<List<Aluno>> ListarPorProfessorAsync(int professorId, CancellationToken ct);
        Task<Aluno?> ObterAlunoPorIdAsync(int id, int professorId, CancellationToken ct);
    }
}
