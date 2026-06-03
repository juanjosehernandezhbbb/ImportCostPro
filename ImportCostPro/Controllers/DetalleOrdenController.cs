using ImportCostPro.Application.DTOs.DetallesOrden;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Persistance.Entities;
using Persistence.Contexts;
using Persistance.Enums; // Asegúrate de tener este using para EstadoOrden

namespace ImportCostPro.Web.Controllers
{
    public class DetalleOrdenController : Controller
    {
        private readonly ImportCostProContext _context;

        public DetalleOrdenController(ImportCostProContext context)
        {
            _context = context;
        }

        // Método auxiliar para validar si la orden permite modificaciones
        private async Task<bool> EsOrdenAbierta(int ordenId)
        {
            var orden = await _context.OrdenesImportacion.FindAsync(ordenId);
            return orden != null && orden.estado == EstadoOrden.Abierta;
        }

        // --- MÉTODOS DE CREACIÓN ---
        [HttpGet]
        public IActionResult Crear(int ordenId)
        {
            var dto = new AgregarProductoOrdenDto { OrdenImportacionId = ordenId };
            ViewBag.Productos = new SelectList(_context.Productos.ToList(), "Id", "Nombre");
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(AgregarProductoOrdenDto dto)
        {
            if (!await EsOrdenAbierta(dto.OrdenImportacionId))
            {
                TempData["Error"] = "No se pueden agregar productos: La orden no está abierta.";
                return RedirectToAction("Editar", "OrdenImportacion", new { id = dto.OrdenImportacionId });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Productos = new SelectList(_context.Productos.ToList(), "Id", "Nombre", dto.ProductoId);
                return View(dto);
            }

            var detalleExistente = await _context.DetallesOrden
                .FirstOrDefaultAsync(d => d.OrdenImportacionId == dto.OrdenImportacionId && d.ProductoId == dto.ProductoId);

            if (detalleExistente != null)
            {
                detalleExistente.Cantidad += dto.Cantidad;
                detalleExistente.PrecioUnitarioFOB = dto.PrecioUnitarioFOB;
                detalleExistente.MargenDeseado = dto.MargenDeseado;
                detalleExistente.Descripcion = dto.Descripcion;
            }
            else
            {
                _context.DetallesOrden.Add(new DetalleOrden
                {
                    OrdenImportacionId = dto.OrdenImportacionId,
                    ProductoId = dto.ProductoId,
                    Cantidad = dto.Cantidad,
                    PrecioUnitarioFOB = dto.PrecioUnitarioFOB,
                    Descripcion = dto.Descripcion,
                    MargenDeseado = dto.MargenDeseado
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Editar", "OrdenImportacion", new { id = dto.OrdenImportacionId });
        }

        // --- MÉTODOS DE EDICIÓN ---
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var detalle = await _context.DetallesOrden.FindAsync(id);
            if (detalle == null) return NotFound();

            var dto = new AgregarProductoOrdenDto
            {
                Id = detalle.Id,
                OrdenImportacionId = detalle.OrdenImportacionId,
                ProductoId = detalle.ProductoId,
                Cantidad = detalle.Cantidad,
                PrecioUnitarioFOB = detalle.PrecioUnitarioFOB,
                Descripcion = detalle.Descripcion,
                MargenDeseado = detalle.MargenDeseado
            };

            ViewBag.Productos = new SelectList(_context.Productos.ToList(), "Id", "Nombre", detalle.ProductoId);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(AgregarProductoOrdenDto dto)
        {
            if (!await EsOrdenAbierta(dto.OrdenImportacionId))
            {
                TempData["Error"] = "No se pueden editar productos: La orden no está abierta.";
                return RedirectToAction("Editar", "OrdenImportacion", new { id = dto.OrdenImportacionId });
            }

            var detalle = await _context.DetallesOrden.FindAsync(dto.Id);
            if (detalle == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Productos = new SelectList(_context.Productos.ToList(), "Id", "Nombre", dto.ProductoId);
                return View(dto);
            }

            detalle.Cantidad = dto.Cantidad;
            detalle.PrecioUnitarioFOB = dto.PrecioUnitarioFOB;
            detalle.MargenDeseado = dto.MargenDeseado;
            detalle.Descripcion = dto.Descripcion;

            await _context.SaveChangesAsync();
            return RedirectToAction("Editar", "OrdenImportacion", new { id = detalle.OrdenImportacionId });
        }

        // --- MÉTODO DE ELIMINACIÓN ---
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id, int ordenId)
        {
            if (!await EsOrdenAbierta(ordenId))
            {
                TempData["Error"] = "No se pueden eliminar productos: La orden no está abierta.";
                return RedirectToAction("Editar", "OrdenImportacion", new { id = ordenId });
            }

            var detalle = await _context.DetallesOrden.FindAsync(id);
            if (detalle != null)
            {
                _context.DetallesOrden.Remove(detalle);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Editar", "OrdenImportacion", new { id = ordenId });
        }
    }
}