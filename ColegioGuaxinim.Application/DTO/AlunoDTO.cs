using System.ComponentModel.DataAnnotations;

namespace ColegioGuaxinim.Application.DTO
{
    public static class AlunoDTO
    {
        public record AlunoListaDto(int Id, string Nome, decimal Mensalidade, DateTime DataVencimento);
        public record AlunoEditarDto(
              [Range(1, int.MaxValue, ErrorMessage = "Id deve ser maior que zero.")] int Id,
              [Required, MinLength(2, ErrorMessage = "O nome deve ter pelo menos 2 caracteres.")] string Nome,
              [Range(0, double.MaxValue, ErrorMessage = "O valor não pode ser negativo.")] decimal Mensalidade,
              DateTime DataVencimento);
        public record AlunoImportLinhaDto(
            [Range(1, int.MaxValue, ErrorMessage = "Id deve ser maior que zero.")] int Id,
            [Required, MinLength(2, ErrorMessage = "O nome deve ter pelo menos 2 caracteres.")] string Nome,
            [Range(0, double.MaxValue, ErrorMessage = "O valor não pode ser negativo.")] decimal Mensalidade,
            DateTime DataVencimento);

    }
}
