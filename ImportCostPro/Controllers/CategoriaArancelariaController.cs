using Microsoft.AspNetCore.Mvc;
using ImportCostPro.Models;

namespace ImportCostPro.Web.Controllers
{
    public class CategoriaArancelariaController : Controller
    {
        public IActionResult Index()
        {
            var categoriasSimuladas = new List<CategoriaArancelariaViewModel>
            {
               
            };

            return View(categoriasSimuladas);
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