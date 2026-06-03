using ImportCostPro.Application.DTOs.Producto;
using ImportCostPro.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Persistence.Contexts;
using Persistance.Enums;

namespace ImportCostPro.Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _service;
        private readonly ImportCostProContext _context;

        public ProductoController(IProductoService service, ImportCostProContext context)
        {
            _service = service;
            _context = context;
        }

        private void PrepararViewBag(object? selectedPais = null, object? selectedCat = null, object? selectedUnidad = null)
        {
            ViewBag.Paises = new SelectList(_context.Set<Persistance.Entities.Pais>().ToList(), "Id", "Nombre", selectedPais);
            ViewBag.Categorias = new SelectList(_context.Set<Persistance.Entities.CategoriaArancelaria>().ToList(), "Id", "Nombre", selectedCat);
            ViewBag.UnidadesMedida = new SelectList(Enum.GetValues<UnidadMedida>().Select(u => new { Value = u, Text = u.ToString() }), "Value", "Text", selectedUnidad);
        }

        public async Task<IActionResult> Index() => View(await _service.ObtenerTodosAsync());

        public IActionResult Crear()
        {
            PrepararViewBag();
            return View(new CrearProductoDto());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearProductoDto dto)
        {
            if (!ModelState.IsValid)
            {
                PrepararViewBag(dto.PaisOrigenID, dto.CategoriaArancelariaID, dto.UnidadDeMedidaID);
                return View(dto);
            }

            await _service.CrearAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var p = await _service.ObtenerPorIdAsync(id);
            if (p == null) return NotFound();

            var dto = new CrearProductoDto
            {
                Nombre = p.Nombre,
                Codigo = p.Codigo,
                PaisOrigenID = p.PaisOrigenID,
                CategoriaArancelariaID = p.CategoriaArancelariaID,
                UnidadDeMedidaID = p.UnidadDeMedidaID,
                PesoUnitario = p.PesoUnitario,
                Largo = p.Largo,
                Ancho = p.Ancho,
                Alto = p.Alto,
                Descripcion = p.Descripcion
            };

            PrepararViewBag(dto.PaisOrigenID, dto.CategoriaArancelariaID, dto.UnidadDeMedidaID);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, CrearProductoDto dto)
        {
            if (!ModelState.IsValid)
            {
                PrepararViewBag(dto.PaisOrigenID, dto.CategoriaArancelariaID, dto.UnidadDeMedidaID);
                return View(dto);
            }
            await _service.ActualizarAsync(id, dto);
            return RedirectToAction(nameof(Index));
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
                TempData["Error"] = "No se pudo eliminar el producto.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}