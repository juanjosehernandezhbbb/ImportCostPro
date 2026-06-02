using Microsoft.AspNetCore.Mvc;
using ImportCostPro.Models;

namespace ImportCostPro.Web.Controllers
{
    public class ImportadorController : Controller
    {
        public IActionResult Index()
        {
            var importadoresSimulados = new List<ImportadorViewModel>
            {
                new ImportadorViewModel { Id = 1, Rnc = "101012345", RazonSocial = "Distribuidora Dominicana SAS", Telefono = "809-555-0199", Contacto = "Carlos Mendoza" },
                new ImportadorViewModel { Id = 2, Rnc = "130987654", RazonSocial = "Importaciones del Caribe SRL", Telefono = "809-555-0244", Contacto = "Ana Martínez" }
            };

            return View(importadoresSimulados);
        }

        public IActionResult Crear()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            return View();
        }

        public IActionResult Eliminar(int id)
        {
            return View();
        }
    }
}