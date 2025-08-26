using ColegioGuaxinim.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace ColegioGuaxinim.Presentation.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IAlunoService _alunoService;
        private readonly IProfessorService _professorService;
        public AlunosController(IAlunoService alunoService, IProfessorService professorService)
        {
            _alunoService = alunoService ?? throw new ArgumentNullException(nameof(alunoService));
            _professorService = professorService ?? throw new ArgumentNullException(nameof(professorService));
        }

        [HttpGet("professor/{professorId}/alunos")]
        public async Task<IActionResult> Lista(int professorId, CancellationToken ct)
        {
            ViewBag.ProfessorId = professorId;
            var alunos = await _alunoService.ListarPorProfessorAsync(professorId, ct);
            return View(alunos);
        }

        [HttpPost("professor/{professorId}/alunos/importar"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Importar(int professorId, IFormFile arquivo, CancellationToken ct)
        {
            var (ins, erros) = await _alunoService.ImportarAsync(professorId, arquivo, ct);
            TempData["Mensagem"] = $"{ins} aluno(s) importado(s).";
            if (erros.Count > 0) TempData["Erros"] = string.Join("<br/>", erros);
            return RedirectToAction(nameof(Index), new { professorId });
        }

        [HttpDelete("professor/{professorId}/alunos/{alunoId}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int professorId, int alunoId, CancellationToken ct)
        {
            var ok = await _alunoService.ExcluirAsync(professorId, alunoId, ct);
            return ok ? NoContent() : NotFound();
        }
    }
}
