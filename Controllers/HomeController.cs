using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PC1_Teoria_Suarez.Models;

namespace PC1_Teoria_Suarez.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Acción para la vista inicial
        public IActionResult Index()
        {
            return View();
        }

        // Acción para manejar la conversión de moneda
        [HttpPost]
        public IActionResult ChangeCurrency(string fromCurrency, string toCurrency, decimal amount)
        {
            decimal rate = 1;

            // Tasa de cambio entre BRL y PEN
            if (fromCurrency == "BRL" && toCurrency == "PEN")
            {
                rate = 0.25m; // Ejemplo de tasa de cambio de BRL a PEN
            }
            else if (fromCurrency == "PEN" && toCurrency == "BRL")
            {
                rate = 4.00m; // Ejemplo de tasa de cambio de PEN a BRL
            }

            decimal result = amount * rate;

            // Redirigir a la acción de boleta con los resultados de la conversión
            return RedirectToAction("Boleta", new { amount = amount, result = result });
        }

        // Acción para mostrar el resultado de la conversión
        public IActionResult Boleta(decimal amount, decimal result)
        {
            // Pasar los valores de la cantidad y el resultado de la conversión a la vista
            ViewBag.Amount = amount;
            ViewBag.Result = result;

            return View();
        }

        // Acción de manejo de errores (esto es estándar)
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
