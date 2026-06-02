using Microsoft.AspNetCore.Mvc;
using ImportCostPro.Models;

namespace ImportCostPro.Web.Controllers
{
    public class ProveedorController : Controller
    {
        public IActionResult Index()
        {
            var proveedoresSimulados = new List<ProveedorViewModel>
            {
                new ProveedorViewModel { Id = 1, Nombre = "Global Tech Logistics", PaisId = 2, NombrePais = "Estados Unidos", Correo = "sales@globaltech.com", Telefono = "+1-555-0144" },
                new ProveedorViewModel { Id = 2, Nombre = "Shenzhen Manufacturing Ltd", PaisId = 3, NombrePais = "China", Correo = "export@shenzhenmfg.cn", Telefono = "+86-755-5555" }
            };

            return View(proveedoresSimulados);
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