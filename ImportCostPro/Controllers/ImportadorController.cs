using Microsoft.AspNetCore.Mvc;
using ImportCostPro.Application.Interfaces;
using ImportCostPro.Application.DTOs.Importadores;

namespace ImportCostPro.Web.Controllers
{
    public class ImportadorController : Controller
    {
        private readonly IImportadorService _importadorService;
        private readonly IPaisService _paisService;

        public ImportadorController(IImportadorService importadorService, IPaisService paisService)
        {
            _importadorService = importadorService;
            _paisService = paisService;
        }

        public async Task<IActionResult> Index()
        {
            var importadores = await _importadorService.ObtenerTodosAsync();
            return View(importadores);
        }


        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Paises = await _paisService.ObtenerTodosAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearImportadorDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Paises = await _paisService.ObtenerTodosAsync();
                return View(dto);
            }

            try
            {
                await _importadorService.CrearAsync(dto);
                TempData["Success"] = "Importador creado exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                ViewBag.Paises = await _paisService.ObtenerTodosAsync();
                return View(dto);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var importador = await _importadorService.ObtenerPorIdAsync(id);

            var dto = new CrearImportadorDto
            {
                NombreRazonSocial = importador.NombreRazonSocial,
                RNC = importador.RNC,
                Telefono = importador.Telefono,
                Correo = importador.Correo,
                Direccion = importador.Direccion,
                PaisID = importador.PaisID
            };

            ViewBag.Paises = await _paisService.ObtenerTodosAsync();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, CrearImportadorDto dto)
        {

            if (id == 0) return NotFound();

            if (!ModelState.IsValid)
            {

                ViewBag.Paises = await _paisService.ObtenerTodosAsync();
                return View(dto);
            }

            try
            {
                await _importadorService.ActualizarAsync(id, dto);
                TempData["Success"] = "Importador actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar: " + ex.Message);
                ViewBag.Paises = await _paisService.ObtenerTodosAsync();
                return View(dto);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _importadorService.EliminarAsync(id);
                TempData["Success"] = "Importador eliminado correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo eliminar: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}