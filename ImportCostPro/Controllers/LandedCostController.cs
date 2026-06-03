using ImportCostPro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Enums;

namespace ImportCostPro.Web.Controllers
{
    public class LandedCostController : Controller
    {
        private readonly ILandedCostService _landedCostService;
        private readonly ImportCostProContext _context;

        public LandedCostController(ILandedCostService landedCostService, ImportCostProContext context)
        {
            _landedCostService = landedCostService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var calculos = await _landedCostService.ObtenerHistoricoCalculosAsync();
            return View(calculos);
        }

        // --- FLUJO DE SELECCIÓN DE ORDEN ---

        [HttpGet]
        public async Task<IActionResult> SeleccionarOrden()
        {
            // Filtramos solo órdenes abiertas para cumplir con la lógica de negocio
            var ordenesAbiertas = await _context.OrdenesImportacion
                .Where(o => o.estado == EstadoOrden.Abierta)
                .ToListAsync();

            return View(ordenesAbiertas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // ESTO ES CLAVE para evitar el error 405
        public IActionResult ProcesarSeleccion(int ordenId)
        {
            if (ordenId <= 0)
            {
                TempData["Error"] = "Debe seleccionar una orden válida.";
                return RedirectToAction(nameof(SeleccionarOrden));
            }

            // Redirige correctamente a la vista de previsualización
            return RedirectToAction("Previsualizar", new { ordenId });
        }

        // --- FLUJO DE CÁLCULO ---

        [HttpGet]
        public async Task<IActionResult> Previsualizar(int ordenId)
        {
            try
            {
                var model = await _landedCostService.PrevisualizarCalculoAsync(ordenId);
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index", "OrdenImportacion");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarOficial(int ordenId)
        {
            var orden = await _context.OrdenesImportacion.FindAsync(ordenId);

            if (orden == null) return NotFound();

            if (orden.estado != EstadoOrden.Abierta)
            {
                TempData["Error"] = "La orden no está en estado Abierta y no puede ser calculada.";
                return RedirectToAction("Index", "OrdenImportacion");
            }

            try
            {
                await _landedCostService.GuardarCalculoOficialAsync(ordenId);
                TempData["Success"] = "Cálculo guardado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al procesar el cálculo: " + ex.Message;
                return RedirectToAction("Previsualizar", new { ordenId });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalle(int id)
        {
            var calculo = await _context.ResultadosCalculo
                .Include(c => c.OrdenImportacion)
                .Include(c => c.DetallesResultadoCalculo)!
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (calculo == null) return NotFound();

            return View(calculo);
        }
    }
}