using ColegioGuaxinim.Domain.Entities;

namespace ColegioGuaxinim.Application.Service
{
    public interface IProfessorService
    {
        Task<List<Professor>> ListarAsync(CancellationToken ct = default);
        Task<Professor?> ObterComAlunosAsync(int id, CancellationToken ct = default);
        Task<Professor?> ObterPorIdAsync(int id, CancellationToken ct = default);
        Task AdicionarAsync(Professor professor, CancellationToken ct = default);
        Task AtualizarAsync(Professor professor, CancellationToken ct = default);
        Task RemoverProfessorAsync(Professor professor, CancellationToken ct = default);
    }
}
