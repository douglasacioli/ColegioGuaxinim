using AutoMapper;
using ColegioGuaxinim.Application.Options;
using ColegioGuaxinim.Domain.Entities;
using ColegioGuaxinim.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Globalization;
using static ColegioGuaxinim.Application.DTO.AlunoDTO;

namespace ColegioGuaxinim.Application.Service
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly ImportacaoAlunosOptions _opt;
        private readonly IMapper _mapper;

        public AlunoService(
         IAlunoRepository alunoRepository,
         IProfessorRepository professorRepository,
         IOptions<ImportacaoAlunosOptions> opt,
         IMapper mapper)
        {
            _alunoRepository = alunoRepository;
            _professorRepository = professorRepository;
            _opt = opt.Value;
            _mapper = mapper;
        }
        public async Task<List<AlunoListaDto>> ListarPorProfessorAsync(int professorId, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();
            var entidades = await _alunoRepository.ListarPorProfessorAsync(professorId, ct);
            return _mapper.Map<List<AlunoListaDto>>(entidades);
        }

        public async Task<bool> ExcluirAsync(int professorId, int alunoId, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            var aluno = await _alunoRepository.ObterAlunoPorIdAsync(alunoId, professorId, ct);
            if (aluno is null) return false;

            await _alunoRepository.RemoverAlunoAsync(alunoId, ct);
            await _alunoRepository.SalvarAlteracoesAsync(ct);
            return true;
        }

        public async Task<(int inseridos, List<string> erros)> ImportarAsync(int professorId, IFormFile arquivo, CancellationToken ct = default)
        {
            ct.ThrowIfCancellationRequested();

            // 1) Professor válido + RN07 (bloqueio)
            var prof = await _professorRepository.ObterPorIdAsync(professorId, ct);
            if (prof is null)
                return (0, new() { "Professor não encontrado." });

            var agora = DateTime.UtcNow;
            if (prof.BloquearTempoDeImportacao.HasValue && prof.BloquearTempoDeImportacao > agora)
            {
                var faltam = prof.BloquearTempoDeImportacao.Value - agora;
                return (0, new() { $"Importação bloqueada. Tente novamente em {Math.Ceiling(faltam.TotalSeconds)}s." });
            }

            // 2) Arquivo
            if (arquivo is null || arquivo.Length == 0)
                return (0, new() { "Arquivo não enviado ou vazio." });

            // 3) Parse das linhas (Nome||Mensalidade||DataVencimento) — RN06
            var erros = new List<string>();
            var novos = new List<Aluno>();
            int inseridos = 0;

            var culture = CultureInfo.GetCultureInfo(_opt.Culture);

            using var sr = new StreamReader(arquivo.OpenReadStream());
            string? linha;
            int n = 0;
            while ((linha = await sr.ReadLineAsync()) is not null)
            {
                ct.ThrowIfCancellationRequested();
                n++;
                if (string.IsNullOrWhiteSpace(linha)) continue;

                var partes = linha.Split(_opt.Delimitador);
                if (partes.Length != 3)
                {
                    erros.Add($"Linha {n}: formato inválido. Use Nome{_opt.Delimitador}Mensalidade{_opt.Delimitador}DataVencimento.");
                    continue;
                }

                var nome = partes[0].Trim();

                if (!decimal.TryParse(partes[1].Trim(),
                    NumberStyles.Number | NumberStyles.AllowCurrencySymbol,
                    culture, out var mensalidade))
                {
                    erros.Add($"Linha {n}: mensalidade inválida.");
                    continue;
                }

                if (!DateTime.TryParseExact(partes[2].Trim(),
                    _opt.FormatoData, culture, DateTimeStyles.None, out var venc))
                {
                    erros.Add($"Linha {n}: data de vencimento inválida.");
                    continue;
                }

                novos.Add(new Aluno
                {
                    Nome = nome,
                    Mensalidade = mensalidade,
                    DataDeVencimento = venc,
                    ProfessorId = professorId
                });
                inseridos++;
            }

            // 4) Persistência dos novos alunos (se houver)
            if (novos.Count > 0)
            {
                await _alunoRepository.AdicionarVariosAsync(novos, ct);
                await _alunoRepository.SalvarAlteracoesAsync(ct);
            }

            // 5) Aplicar bloqueio (RN07)
            prof.BloquearTempoDeImportacao = DateTime.UtcNow.AddSeconds(_opt.DuracaoBloqueioSegundos);
            await _professorRepository.AtualizarAsync(prof, ct);
            await _alunoRepository.SalvarAlteracoesAsync(ct);

            return (inseridos, erros);
        }

    }
}
