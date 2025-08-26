using ColegioGuaxinim.Application.Service;
using Microsoft.AspNetCore.Mvc;
using static ColegioGuaxinim.Application.DTO.ProfessorDTO;

namespace ColegioGuaxinim.Presentation.Controllers
{
    public class ProfessoresController : Controller
    {
        private readonly IProfessorService _professorService;

        public ProfessoresController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        // RN01 - Página inicial com lista de professores
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var professores = await _professorService.ListarAsync(ct);
            return View(professores);
        }

        // RN03 - GET para exibir tela de cadastro
        public IActionResult Criar()
        {
            return View();
        }

        // RN03 - POST para cadastrar professor
        [HttpPost]
        public async Task<IActionResult> Criar(ProfessorCriarDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(dto);

            await _professorService.CriarAsync(dto, ct);
            return RedirectToAction(nameof(Index));
        }

        // RN08 - Excluir professor (opcional)
        [HttpPost]
        public async Task<IActionResult> Excluir(int id, CancellationToken ct)
        {
            await _professorService.ExcluirAsync(id, ct);
            return RedirectToAction(nameof(Index));
        }
    }
}
