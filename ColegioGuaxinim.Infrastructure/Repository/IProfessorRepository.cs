using ColegioGuaxinim.Domain.Entities;

namespace ColegioGuaxinim.Infrastructure.Repository
{
    public interface IProfessorRepository
    {
        Task<List<Professor>> ListarAsync(CancellationToken ct = default);
        Task<Professor?> ObterComAlunosAsync(int id, CancellationToken ct = default);
        Task<Professor?> ObterPorIdAsync(int id, CancellationToken ct = default);
        Task<Professor> AdicionarAsync(Professor professor, CancellationToken ct = default);
        Task<Professor> AtualizarAsync(Professor professor, CancellationToken ct = default);
        Task<bool> RemoverProfessorAsync(int id, CancellationToken ct = default);
        Task SalvarAlteracoesAsync(CancellationToken ct = default);
    }
}
