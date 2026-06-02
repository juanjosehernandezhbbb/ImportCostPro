using Microsoft.AspNetCore.Mvc;
using ImportCostPro.Models;

namespace ImportCostPro.Web.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            var productosSimulados = new List<ProductoViewModel>
            {
                new ProductoViewModel { Id = 1, Codigo = "PROD-RECU-001", Nombre = "Amuleto Brújula Caprichosa", Descripcion = "Indica la posición actual en el mapa.", Activo = true },
                new ProductoViewModel { Id = 2, Codigo = "PROD-RECU-002", Nombre = "Pluma de Mapas", Descripcion = "Permite actualizar los mapas con nuevas salas.", Activo = true },
                new ProductoViewModel { Id = 3, Codigo = "PROD-RECU-003", Nombre = "Linterna de Lumélula", Descripcion = "Ilumina las áreas más oscuras de los caminos.", Activo = false }
            };

            return View(productosSimulados);
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