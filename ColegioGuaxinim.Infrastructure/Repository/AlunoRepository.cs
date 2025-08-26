using ColegioGuaxinim.Domain.Entities;
using ColegioGuaxinim.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ColegioGuaxinim.Infrastructure.Repository
{
    public class AlunoRepository : IAlunoRepository
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

        public async Task<Aluno> AtualizarAsync(Aluno aluno, CancellationToken ct = default)
        {
            var alunoAtual = await _context.Alunos.FirstOrDefaultAsync(p => p.Id == aluno.Id, ct);

            if (alunoAtual == null)
                throw new Exception("Aluno não encontrado");

            alunoAtual.Nome = aluno.Nome;
            alunoAtual.Mensalidade = aluno.Mensalidade;
            alunoAtual.DataDeVencimento = aluno.DataDeVencimento;
            await _context.SaveChangesAsync(ct);
            return alunoAtual;
        }

        public Task<List<Aluno>> ListarPorProfessorAsync(int professorId, CancellationToken ct = default)
        {
            return _context.Alunos.AsNoTracking().Where(p => p.ProfessorId == professorId).OrderBy(a => a.Nome).ToListAsync(ct);
        }

        public Task<Aluno?> ObterAlunoPorIdAsync(int id, int professorId, CancellationToken ct = default)
        {
            return _context.Alunos.FirstOrDefaultAsync(a => a.Id == id && a.ProfessorId == professorId, ct);
        }

        public async Task<bool> RemoverAlunoAsync(int id, CancellationToken ct = default)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(p => p.Id == id, ct);

            if (aluno == null)
                throw new Exception("Aluno não encontrado");

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync(ct);
            return true;
        }
       

        public Task SalvarAlteracoesAsync(CancellationToken ct = default)
        {
            return _context.SaveChangesAsync(ct);
        }
       
    }
}
