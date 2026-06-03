using Microsoft.AspNetCore.Mvc;
using ImportCostPro.Application.Interfaces;
using ImportCostPro.Models;
using ImportCostPro.Application.DTOs.Impuestos;

namespace ImportCostPro.Web.Controllers
{
    public class ImpuestoController : Controller
    {

        private readonly IConfigImpuestosService _configImpuestosService;


        public ImpuestoController(IConfigImpuestosService configImpuestosService)
        {
            _configImpuestosService = configImpuestosService;
        }

        public async Task<IActionResult> Index()
        {

            var dto = await _configImpuestosService.ObtenerConfiguracionAsync();

            var vm = new ImpuestoViewModel();
            if (dto != null)
            {
                vm.Id = dto.Id;
                vm.PorcentajeITBIS = dto.PorcentajeITBIS;
                vm.PorcentajeTasaServicioAduanal = dto.PorcentajeTasaServicioAduanal;
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(ImpuestoViewModel vm)
        {
            if (!ModelState.IsValid) return View("Index", vm);

            try
            {
                var dto = new ConfiguracionImpuestosCreateDto
                {
                    PorcentajeITBIS = vm.PorcentajeITBIS,
                    PorcentajeTasaServicioAduanal = vm.PorcentajeTasaServicioAduanal
                };


                await _configImpuestosService.GuardarConfiguracionAsync(dto);

                TempData["Success"] = "Configuración de impuestos guardada exitosamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al guardar: " + ex.Message;
                return View("Index", vm);
            }
        }
    }
}