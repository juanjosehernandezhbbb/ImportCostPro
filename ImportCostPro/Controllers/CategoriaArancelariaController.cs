using ImportCostPro.Application.DTOs.CategoriasArancelarias;
using ImportCostPro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImportCostPro.Web.Controllers
{
    public class CategoriaArancelariaController : Controller
    {
        private readonly ICategoriaArancelariaService _service;

        public CategoriaArancelariaController(ICategoriaArancelariaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index() => View(await _service.ObtenerTodasAsync());

        public IActionResult Crear() => View();

        [HttpPost]
        public async Task<IActionResult> Crear(CategoriaArancelariaCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            try
            {
                await _service.CrearAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var c = await _service.ObtenerPorIdAsync(id);
            if (c == null) return NotFound();

            var dto = new CategoriaArancelariaCreateDto
            {
                Codigo = c.Codigo,
                Nombre = c.Nombre,
                PorcentajeArancel = c.PorcentajeArancel,
                AplicaITBIS = c.AplicaITBIS,
                AplicaImpuestoSelectivo = c.AplicaImpuestoSelectivo,
                PorcentajeImpuestoSelectivo = c.PorcentajeImpuestoSelectivo,
                Activo = c.Activo
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, CategoriaArancelariaCreateDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            try
            {
                await _service.ActualizarAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            try
            {
                await _service.EliminarAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Error"] = "No se pudo eliminar la categoría.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}