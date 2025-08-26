using ColegioGuaxinim.Domain.Entities;
using ColegioGuaxinim.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ColegioGuaxinim.Infrastructure.Repository
{
    public class AlunoRepository : IAlunoRepositoryReader, IAlunoRepositoryWriter
    {
        private readonly GuaxinimDbContext _context;

        public AlunoRepository(GuaxinimDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarVariosAsync(IEnumerable<Aluno> alunos, CancellationToken ct = default)
        {
            if (alunos is null) throw new ArgumentNullException(nameof(alunos));
            var lista = alunos as IList<Aluno> ?? alunos.ToList();
            if (lista.Count == 0) return;

            await _context.Alunos.AddRangeAsync(lista, ct);

        }

        public Task<List<Aluno>> ListarPorProfessorAsync(int professorId, CancellationToken ct = default)
        {
            return _context.Alunos.AsNoTracking().Where(p => p.ProfessorId == professorId).OrderBy(a => a.Nome).ToListAsync(ct);
        }

        public Task<Aluno?> ObterAlunoPorIdAsync(int id, int professorId, CancellationToken ct = default)
        {
            return _context.Alunos.FirstOrDefaultAsync(a => a.Id == id && a.ProfessorId == professorId, ct);
        }

        public Task RemoverAlunoAsync(Aluno aluno, CancellationToken ct = default)
        {
            if (aluno is null) throw new ArgumentNullException(nameof(aluno));
            _context.Alunos.Remove(aluno);
            return Task.CompletedTask;
        }

        public Task SalvarAlteracoesAsync(CancellationToken ct = default)
        {
            return _context.SaveChangesAsync(ct);
        }
    }
}
