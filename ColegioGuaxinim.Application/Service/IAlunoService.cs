using Microsoft.AspNetCore.Http;
using static ColegioGuaxinim.Application.DTO.AlunoDTO;

namespace ColegioGuaxinim.Application.Service
{
    public interface IAlunoService
    {
        Task<List<AlunoListaDto>> ListarPorProfessorAsync(int professorId, CancellationToken ct = default);
        Task<bool> ExcluirAsync(int professorId, int alunoId, CancellationToken ct = default);
        Task<(int inseridos, List<string> erros)> ImportarAsync(int professorId, IFormFile arquivo, CancellationToken ct = default);
    }
}
