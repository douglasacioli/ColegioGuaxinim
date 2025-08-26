using ColegioGuaxinim.Domain.Entities;

namespace ColegioGuaxinim.Infrastructure.Repository
{
    public interface IProfessorRepositoryReader
    {
        Task<List<Professor>> ListarAsync(CancellationToken ct = default);
        Task<Professor?> ObterComAlunosAsync(int id, CancellationToken ct = default);
        Task<Professor?> ObterPorIdAsync(int id, CancellationToken ct = default);

    }
}
