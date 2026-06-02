using Microsoft.AspNetCore.Mvc;
using ImportCostPro.Models;

namespace ImportCostPro.Web.Controllers
{
    public class MonedaController : Controller
    {
        public IActionResult Index()
        {
            var monedasSimuladas = new List<MonedaViewModel>
            {

            };

            return View(monedasSimuladas);
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