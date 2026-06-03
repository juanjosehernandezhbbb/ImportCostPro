using Microsoft.AspNetCore.Mvc;
using ImportCostPro.Application.Interfaces;
using ImportCostPro.Models;
using ImportCostPro.Application.DTOs.Pais;

namespace ImportCostPro.Web.Controllers
{
    public class PaisController : Controller
    {
        private readonly IPaisService _paisService;

        public PaisController(IPaisService paisService) => _paisService = paisService;

        public async Task<IActionResult> Index()
        {
            var paisesDto = await _paisService.ObtenerTodosAsync();
            var paisesVm = paisesDto.Select(p => new PaisViewModel
            {
                Id = p.Id,
                Nombre = p.Nombre,
                CodigoISO = p.CodigoISO,
                Activo = p.Estado
            }).ToList();

            return View(paisesVm);
        }

        public IActionResult Crear() => View(new PaisViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken] // Seguridad esencial
        public async Task<IActionResult> Crear(PaisViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _paisService.CrearAsync(new CrearPaisDto { Nombre = vm.Nombre, CodigoISO = vm.CodigoISO });
                TempData["Success"] = "País registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            try
            {
                var dto = await _paisService.ObtenerPorIdAsync(id);
                return View(new PaisViewModel { Id = dto.Id, Nombre = dto.Nombre, CodigoISO = dto.CodigoISO, Activo = dto.Estado });
            }
            catch
            {
                TempData["Error"] = "No se pudo cargar la información del país.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Seguridad esencial
        public async Task<IActionResult> Editar(int id, PaisViewModel vm) // Recibe id por seguridad
        {
            if (id != vm.Id) return NotFound(); // Validación extra
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _paisService.ActualizarAsync(vm.Id, new CrearPaisDto { Nombre = vm.Nombre, CodigoISO = vm.CodigoISO });
                TempData["Success"] = "País actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _paisService.EliminarAsync(id);
                TempData["Success"] = "País eliminado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}