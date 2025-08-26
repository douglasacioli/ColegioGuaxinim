using ColegioGuaxinim.Domain.Entities;

namespace ColegioGuaxinim.Infrastructure.Repository
{
    public interface IProfessorRepositoryWriter
    {
        Task AdicionarAsync(Professor professor, CancellationToken ct = default);
        Task AtualizarAsync(Professor professor, CancellationToken ct = default);
        Task RemoverProfessorAsync(Professor professor, CancellationToken ct = default);
        Task SalvarAlteracoesAsync(CancellationToken ct = default);
    }
}
