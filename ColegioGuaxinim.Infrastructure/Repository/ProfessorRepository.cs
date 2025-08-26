using ColegioGuaxinim.Domain.Entities;
using ColegioGuaxinim.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ColegioGuaxinim.Infrastructure.Repository
{
    public class ProfessorRepository : IProfessorRepository
    
    {
        private readonly GuaxinimDbContext _context;

        public ProfessorRepository(GuaxinimDbContext conetext)
        {
            _context = conetext;
        }

        public Task<List<Professor>> ListarAsync(CancellationToken ct = default)
        {
            return _context.Professores.AsNoTracking().OrderBy(p => p.Nome).ToListAsync(ct);
        }

        public Task<Professor?> ObterComAlunosAsync(int id, CancellationToken ct = default)
        {
            return _context.Professores.Include(p => p.Alunos).FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public Task<Professor?> ObterPorIdAsync(int id, CancellationToken ct = default)
        {
            return _context.Professores.FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public async Task<Professor> AdicionarAsync(Professor professor, CancellationToken ct)
        {
            await _context.Professores.AddAsync(professor, ct);
            await _context.SaveChangesAsync(ct);
            return professor;
        }

        public async Task<Professor> AtualizarAsync(Professor professor, CancellationToken ct = default)
        {
            var professoresAtual = await _context.Professores.FirstOrDefaultAsync(p => p.Id == professor.Id, ct);

            if (professoresAtual == null)
                throw new Exception("Professor não encontrado");

            professoresAtual.Nome = professor.Nome;
            professoresAtual.BloquearTempoDeImportacao = professor.BloquearTempoDeImportacao;
            await _context.SaveChangesAsync(ct);
            return professoresAtual;
        }

        public async Task<bool> RemoverProfessorAsync(int id, CancellationToken ct = default)
        {
            var professor = await _context.Professores.FirstOrDefaultAsync(p => p.Id == id, ct);

            if (professor == null)
                throw new Exception("Professor Não encontrado");

            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync(ct);
            return true;
        }


        public Task SalvarAlteracoesAsync(CancellationToken ct = default)
        {
           return _context.SaveChangesAsync(ct);
        }
       
    }
}
