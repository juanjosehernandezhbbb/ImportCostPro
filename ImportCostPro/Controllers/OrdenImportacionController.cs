using ImportCostPro.Application.DTOs.Ordenes;
using ImportCostPro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Persistence.Contexts;
using Persistance.Enums;

namespace ImportCostPro.Web.Controllers
{
    public class OrdenImportacionController : Controller
    {
        private readonly IOrdenImportacionService _ordenService;
        private readonly IDetalleOrdenService _detalleService;
        private readonly ImportCostProContext _context;

        public OrdenImportacionController(IOrdenImportacionService ordenService, IDetalleOrdenService detalleService, ImportCostProContext context)
        {
            _ordenService = ordenService;
            _detalleService = detalleService;
            _context = context;
        }

        private void PrepararViewBag(EstadoOrden? estadoSeleccionado = null)
        {
            ViewBag.Paises = new SelectList(_context.Paises.ToList(), "Id", "Nombre");
            ViewBag.Monedas = new SelectList(_context.Monedas.ToList(), "Id", "Nombre");
            ViewBag.Importadores = new SelectList(_context.Importadores.ToList(), "Id", "NombreRazonSocial");
            ViewBag.Proveedores = new SelectList(_context.Proveedores.ToList(), "Id", "Nombre");
            ViewBag.Modalidades = new SelectList(Enum.GetValues(typeof(ModalidadTransporte)));
            ViewBag.Estados = new SelectList(Enum.GetValues(typeof(EstadoOrden)), estadoSeleccionado);
        }

        public async Task<IActionResult> Index(string filtroEstado = "Activas")
        {
            // Obtienes todas las órdenes
            var todas = await _ordenService.ObtenerTodasAsync();

            // Guardas el filtro para que el botón sepa cuál está activo
            ViewBag.FiltroActual = filtroEstado;

            if (filtroEstado == "Historico")
            {
                // Filtro para mostrar solo las que ya terminaron su ciclo
                return View(todas.Where(o => o.Estado == EstadoOrden.Calculada || o.Estado == EstadoOrden.Cerrada));
            }

            // Filtro por defecto (Solo órdenes que permiten edición)
            return View(todas.Where(o => o.Estado == EstadoOrden.Abierta));
        }

        [HttpGet]
        public IActionResult Crear()
        {
            // Esto prepara los dropdowns (Paises, Monedas, etc.)
            PrepararViewBag();

            // Retorna el formulario vacío
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearOrdenDto dto)
        {
            if (!ModelState.IsValid)
            {
                PrepararViewBag();
                return View(dto);
            }
            await _ordenService.CrearAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            if (id <= 0)
            {
                TempData["Error"] = "ID de orden no válido.";
                return RedirectToAction(nameof(Index));
            }

            var orden = await _ordenService.ObtenerPorIdAsync(id);

            if (orden == null)
            {
                TempData["Error"] = $"No existe una orden con el ID {id}.";
                return RedirectToAction(nameof(Index));
            }

            // --- AGREGA ESTA LÍNEA PARA QUE LA VISTA SEPA EL ESTADO ---
            ViewBag.EstadoOrden = orden.Estado;
            // --------------------------------------------------------

            ViewBag.Detalles = await _detalleService.ObtenerPorOrdenIdAsync(id) ?? new List<ImportCostPro.Application.DTOs.DetallesOrden.DetalleOrdenDto>();

            PrepararViewBag(orden.Estado);

            return View(new CrearOrdenDto
            {
                NumeroOrden = orden.NumeroOrden,
                PaisOrigenID = orden.PaisOrigenID,
                MonedaID = orden.MonedaID,
                ImportadorID = orden.ImportadorID,
                ProveedorID = orden.ProveedorID,
                FechaOrden = orden.FechaOrden,
                ModalidadTransporte = orden.ModalidadTransporte,
                EstadoOrden = orden.Estado
            });
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, CrearOrdenDto dto)
        {
            if (!ModelState.IsValid)
            {
                PrepararViewBag(dto.EstadoOrden);
                ViewBag.Detalles = await _detalleService.ObtenerPorOrdenIdAsync(id);
                return View(dto);
            }

            try
            {
                await _ordenService.ActualizarAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar: " + ex.Message);
                PrepararViewBag(dto.EstadoOrden);
                ViewBag.Detalles = await _detalleService.ObtenerPorOrdenIdAsync(id);
                return View(dto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int id, EstadoOrden estado)
        {
            try
            {
                await _ordenService.CambiarEstadoAsync(id, estado);
                TempData["Success"] = "Estado actualizado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cambiar estado: " + ex.Message;
            }
            return RedirectToAction(nameof(Editar), new { id });
        }
    }
}