using ImportCostPro.Application.DTOs.Gasto;
using ImportCostPro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistance.Enums;

namespace ImportCostPro.Web.Controllers
{
    public class GastoImportacionController : Controller
    {
        private readonly IGastoImportacionService _gastoService;
        private readonly ImportCostProContext _context;

        public GastoImportacionController(IGastoImportacionService gastoService, ImportCostProContext context)
        {
            _gastoService = gastoService;
            _context = context;
        }

        // Método de seguridad para validar el estado según el documento funcional
        private async Task<bool> EsOrdenEditable(int ordenId)
        {
            var orden = await _context.OrdenesImportacion.FindAsync(ordenId);
            return orden != null && orden.estado == EstadoOrden.Abierta;
        }

        private void CargarMonedas(int? selectedValue = null)
        {
            ViewBag.Monedas = new SelectList(_context.Monedas.ToList(), "Id", "Nombre", selectedValue);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listaGastos = await _context.GastosImportacion
                .Include(g => g.OrdenImportacion)
                .OrderByDescending(g => g.FechaGasto)
                .ToListAsync();

            var gastosDto = listaGastos.Select(g => new GastoImportacionDto
            {
                Id = g.Id,
                FechaGasto = g.FechaGasto,
                TipoGasto = g.TipoGasto,
                Monto = g.Monto,
                MetodoDistribucion = g.MetodoDistribucion,
                NumeroOrden = g.OrdenImportacion?.NumeroOrden ?? "N/A"
            }).ToList();

            return View(gastosDto);
        }

        [HttpGet]
        public IActionResult SeleccionarOrden()
        {
            var ordenesActivas = _context.OrdenesImportacion
                .Where(o => o.estado == EstadoOrden.Abierta)
                .Select(o => new { o.Id, o.NumeroOrden })
                .ToList();

            ViewBag.Ordenes = new SelectList(ordenesActivas, "Id", "NumeroOrden");
            return View();
        }

        [HttpGet]
        public IActionResult Crear(int ordenId)
        {
            if (ordenId <= 0) return RedirectToAction(nameof(SeleccionarOrden));
            CargarMonedas();
            return View(new AgregarGastoDto { OrdenImportacionId = ordenId, FechaGasto = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(AgregarGastoDto dto)
        {
            if (!await EsOrdenEditable(dto.OrdenImportacionId))
            {
                TempData["Error"] = "No se pueden agregar gastos: La orden no está abierta.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                CargarMonedas(dto.MonedaID);
                return View(dto);
            }

            try
            {
                await _gastoService.AgregarGastoAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error: {ex.Message}";
                CargarMonedas(dto.MonedaID);
                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var gasto = await _context.GastosImportacion.FindAsync(id);
            if (gasto == null) return NotFound();

            CargarMonedas(gasto.MonedaID);

            return View(new AgregarGastoDto
            {
                OrdenImportacionId = gasto.OrdenImportacionId,
                MonedaID = gasto.MonedaID,
                Monto = gasto.Monto,
                FechaGasto = gasto.FechaGasto,
                TipoGasto = gasto.TipoGasto,
                MetodoDistribucion = gasto.MetodoDistribucion
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, AgregarGastoDto dto)
        {
            if (!await EsOrdenEditable(dto.OrdenImportacionId))
            {
                TempData["Error"] = "No se pueden editar gastos: La orden no está abierta.";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                CargarMonedas(dto.MonedaID);
                return View(dto);
            }

            try
            {
                await _gastoService.ActualizarGastoAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al actualizar: " + ex.Message;
                CargarMonedas(dto.MonedaID);
                return View(dto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var gasto = await _context.GastosImportacion.FindAsync(id);
            if (gasto != null)
            {
                if (!await EsOrdenEditable(gasto.OrdenImportacionId))
                {
                    TempData["Error"] = "No se pueden eliminar gastos: La orden no está abierta.";
                    return RedirectToAction(nameof(Index));
                }

                try { await _gastoService.EliminarGastoAsync(id); }
                catch (Exception ex) { TempData["Error"] = ex.Message; }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}