using Microsoft.AspNetCore.Mvc;
using ImportCostPro.Models;

namespace ImportCostPro.Web.Controllers
{
    public class TasaCambioController : Controller
    {
        public IActionResult Index()
        {
            var tasasSimuladas = new List<TasaCambioViewModel>
            {
                new TasaCambioViewModel { Id = 1, MonedaOrigenId = 2, CodigoMonedaOrigen = "USD", MonedaDestinoId = 1, CodigoMonedaDestino = "DOP", ValorTasa = 59.25m, Fecha = DateTime.Today },
                new TasaCambioViewModel { Id = 2, MonedaOrigenId = 3, CodigoMonedaOrigen = "EUR", MonedaDestinoId = 1, CodigoMonedaDestino = "DOP", ValorTasa = 63.80m, Fecha = DateTime.Today }
            };

            return View(tasasSimuladas);
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