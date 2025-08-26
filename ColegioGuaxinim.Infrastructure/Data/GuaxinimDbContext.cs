using ColegioGuaxinim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColegioGuaxinim.Infrastructure.Data
{
    public class GuaxinimDbContext : DbContext
    {
        public GuaxinimDbContext(DbContextOptions<GuaxinimDbContext> options) : base(options){}
        public DbSet<Professor> Professores => Set<Professor>();
        public DbSet<Aluno> Alunos => Set<Aluno>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Professor>(e =>
            {
                e.ToTable("Professor");
                e.HasKey(x => x.Id);
                e.Property(x => x.Nome).IsRequired().HasMaxLength(120);
                e.Property(x => x.BloquearTempoDeImportacao).HasColumnName("BloquearTempoDeImportacao").HasColumnType("datetime2").IsRequired(false);
                e.HasMany(x => x.Alunos).WithOne(a => a.Professor).HasForeignKey(a => a.ProfessorId);
            });

            mb.Entity<Aluno>(e =>
            {
                e.ToTable("Aluno");
                e.HasKey(x => x.Id);
                e.Property(x => x.Nome).IsRequired().HasMaxLength(120);
                e.Property(x => x.Mensalidade).HasPrecision(18, 2);
                e.Property(x => x.DataDeVencimento).IsRequired();
            });
        }
    }
}
