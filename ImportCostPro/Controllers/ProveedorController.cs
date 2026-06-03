using ImportCostPro.Application.DTOs.Proveedores;
using ImportCostPro.Application.Interfaces;
using Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ImportCostPro.Web.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly IProveedorService _service;
        private readonly ImportCostProContext _context;

        public ProveedorController(IProveedorService service, ImportCostProContext context)
        {
            _service = service;
            _context = context;
        }

        private void CargarCatalogos(int? paisId = null, int? monedaId = null)
        {
            ViewBag.Paises = new SelectList(_context.Set<Persistance.Entities.Pais>().ToList(), "Id", "Nombre", paisId);
            ViewBag.Monedas = new SelectList(_context.Set<Persistance.Entities.Moneda>().ToList(), "Id", "Nombre", monedaId);
        }

        public async Task<IActionResult> Index() => View(await _service.ObtenerTodosAsync());

        public IActionResult Crear()
        {
            CargarCatalogos();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearProveedorDto dto)
        {
            if (!ModelState.IsValid)
            {
                CargarCatalogos(dto.PaisOrigenID, dto.MonedaPrincipalID);
                return View(dto);
            }
            await _service.CrearAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Editar(int id)
        {
            var p = await _service.ObtenerPorIdAsync(id);
            if (p == null) return NotFound();

            var dto = new CrearProveedorDto
            {
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                Correo = p.Correo,
                Direccion = p.Direccion,
                PaisOrigenID = p.PaisOrigenID,
                MonedaPrincipalID = p.MonedaPrincipalID
            };

            CargarCatalogos(p.PaisOrigenID, p.MonedaPrincipalID);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, CrearProveedorDto dto)
        {
            if (!ModelState.IsValid)
            {
                CargarCatalogos(dto.PaisOrigenID, dto.MonedaPrincipalID);
                return View(dto);
            }

            await _service.ActualizarAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            await _service.EliminarAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}