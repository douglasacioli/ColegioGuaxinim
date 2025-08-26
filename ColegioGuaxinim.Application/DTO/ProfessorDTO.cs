using System.ComponentModel.DataAnnotations;

namespace ColegioGuaxinim.Application.DTO
{
    public static class ProfessorDTO
    {
        public record ProfessorListaDto(int Id, string Nome);
        public record ProfessorDetalheDto(int Id, string Nome, int QuantidadeAlunos);
        public record ProfessorCriarDto(
            [Required, MinLength(2, ErrorMessage = "O nome deve ter pelo menos 2 caracteres.")] string Nome);
        public record ProfessorEditarDto(
            [Range(1, int.MaxValue, ErrorMessage = "Id deve ser maior que zero.")] int Id,
            [Required, MinLength(2, ErrorMessage = "O nome deve ter pelo menos 2 caracteres.")] string Nome);
    }
}
