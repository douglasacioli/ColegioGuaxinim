using ColegioGuaxinim.Domain.Entities;
using static ColegioGuaxinim.Application.DTO.ProfessorDTO;

namespace ColegioGuaxinim.Application.Service
{
    public interface IProfessorService
    {
        Task<List<ProfessorListaDto>> ListarAsync(CancellationToken ct = default);
        Task<ProfessorDetalheDto?> ObterAsync(int id, CancellationToken ct = default);
        Task<int> CriarAsync(ProfessorCriarDto dto, CancellationToken ct = default);
        Task<bool> EditarAsync(ProfessorEditarDto dto, CancellationToken ct = default);
        Task<bool> ExcluirAsync(int id, CancellationToken ct = default);
    }
}
