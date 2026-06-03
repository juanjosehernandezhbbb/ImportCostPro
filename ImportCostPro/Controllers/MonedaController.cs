using Microsoft.AspNetCore.Mvc;
using ImportCostPro.Application.Interfaces;
using ImportCostPro.Models;
using ImportCostPro.Application.DTOs.Moneda;

namespace ImportCostPro.Web.Controllers
{
    public class MonedaController : Controller
    {
        private readonly IMonedaService _monedaService;

        public MonedaController(IMonedaService monedaService) => _monedaService = monedaService;

        public async Task<IActionResult> Index()
        {
            var dtos = await _monedaService.ObtenerTodasAsync();
            var vms = dtos.Select(d => new MonedaViewModel
            {
                Id = d.Id,
                Nombre = d.Nombre,
                CodigoISO = d.CodigoISO,
                Simbolo = d.Simbolo,
                EsMonedaLocal = d.EsMonedaLocal,
                Activo = d.Activo
            }).ToList();
            return View(vms);
        }

        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(MonedaViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _monedaService.CrearAsync(new CrearMonedaDTO
                {
                    Nombre = vm.Nombre,
                    CodigoISO = vm.CodigoISO,
                    Simbolo = vm.Simbolo,
                    EsMonedaLocal = vm.EsMonedaLocal
                });
                TempData["Success"] = "Moneda creada exitosamente.";
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
                var dto = await _monedaService.ObtenerPorIdAsync(id);
                return View(new MonedaViewModel
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    CodigoISO = dto.CodigoISO,
                    Simbolo = dto.Simbolo,
                    EsMonedaLocal = dto.EsMonedaLocal,
                    Activo = dto.Activo
                });
            }
            catch
            {
                TempData["Error"] = "No se pudo encontrar la moneda.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(MonedaViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _monedaService.ActualizarAsync(new MonedaDto
                {
                    Id = vm.Id,
                    Nombre = vm.Nombre,
                    CodigoISO = vm.CodigoISO,
                    Simbolo = vm.Simbolo,
                    EsMonedaLocal = vm.EsMonedaLocal,
                    Activo = vm.Activo
                });
                TempData["Success"] = "Moneda actualizada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _monedaService.EliminarAsync(id);
                TempData["Success"] = "Moneda eliminada correctamente.";
            }
            catch (Exception ex)
            {
                // Aquí capturamos el error de "No se puede eliminar moneda local"
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}