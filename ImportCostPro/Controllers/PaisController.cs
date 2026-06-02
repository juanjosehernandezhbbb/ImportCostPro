using Microsoft.AspNetCore.Mvc;
using ImportCostPro.Models;

namespace ImportCostPro.Web.Controllers
{
    public class PaisController : Controller
    {
        public IActionResult Index()
        {
            var paisesSimulados = new List<PaisViewModel>
            {

            };

            return View(paisesSimulados);
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