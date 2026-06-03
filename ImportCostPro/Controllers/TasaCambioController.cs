using ImportCostPro.Application.DTOs.TasasCambio;
using ImportCostPro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Persistence.Contexts;

namespace ImportCostPro.Web.Controllers
{
    public class TasaCambioController : Controller
    {
        private readonly ITasaCambioService _service;
        private readonly ImportCostProContext _context;

        public TasaCambioController(ITasaCambioService service, ImportCostProContext context)
        {
            _service = service;
            _context = context;
        }

        private void PrepararViewBag()
        {
            ViewBag.Monedas = new SelectList(_context.Monedas.ToList(), "Id", "Nombre");
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.ObtenerTodasAsync());
        }

        public IActionResult Crear()
        {
            PrepararViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearTasaCambioDto dto)
        {
            if (!ModelState.IsValid)
            {
                PrepararViewBag();
                return View(dto);
            }

            try
            {
                await _service.CrearAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                PrepararViewBag();
                return View(dto);
            }
        }

        public async Task<IActionResult> Editar(int id)
        {
            var tasa = await _service.ObtenerPorIdAsync(id);
            PrepararViewBag();
            return View(tasa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, CrearTasaCambioDto dto)
        {
            if (!ModelState.IsValid)
            {
                PrepararViewBag();
                return View(dto);
            }

            try
            {
                await _service.ActualizarAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                PrepararViewBag();
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
            catch (Exception)
            {
                TempData["Error"] = "No se pudo eliminar la tasa de cambio.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}