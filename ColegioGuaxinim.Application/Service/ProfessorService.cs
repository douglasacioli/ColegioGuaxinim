using AutoMapper;
using ColegioGuaxinim.Domain.Entities;
using ColegioGuaxinim.Infrastructure.Repository;
using static ColegioGuaxinim.Application.DTO.ProfessorDTO;

namespace ColegioGuaxinim.Application.Service
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;

        public ProfessorService(IProfessorRepository professorRepository, IMapper mapper)
        {
            _professorRepository = professorRepository ?? throw new ArgumentNullException(nameof(professorRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> CriarAsync(ProfessorCriarDto dto, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var entidade = _mapper.Map<Professor>(dto);
            await _professorRepository.AdicionarAsync(entidade, ct);
            await _professorRepository.SalvarAlteracoesAsync(ct);
            return entidade.Id;
        }

        public async Task<bool> EditarAsync(ProfessorEditarDto dto, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            if (dto is null) throw new ArgumentNullException(nameof(dto));

            var existente = await _professorRepository.ObterPorIdAsync(dto.Id, ct);
            if (existente is null) return false;

            _mapper.Map(dto, existente);

            await _professorRepository.AtualizarAsync(existente, ct);
            await _professorRepository.SalvarAlteracoesAsync(ct);
            return true;
        }

        public async Task<bool> ExcluirAsync(int id, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            if (id <= 0) throw new ArgumentException("ID inválido.");
            var ok = await _professorRepository.RemoverProfessorAsync(id, ct);
            if (!ok) return false;

            await _professorRepository.SalvarAlteracoesAsync(ct);
            return true;

        }

        public async Task<List<ProfessorListaDto>> ListarAsync(CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            var entidades = await _professorRepository.ListarAsync(ct);
            return _mapper.Map<List<ProfessorListaDto>>(entidades);
        }

        public async Task<ProfessorDetalheDto?> ObterAsync(int id, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            if (id <= 0) throw new ArgumentException("ID inválido.", nameof(id));
            var entidade = await _professorRepository.ObterComAlunosAsync(id, ct);
            return entidade is null ? null : _mapper.Map<ProfessorDetalheDto>(entidade);
        }
    }
}
