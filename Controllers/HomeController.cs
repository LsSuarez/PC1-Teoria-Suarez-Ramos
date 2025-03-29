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

            // Tasa de cambio entre BRL, PEN y USD
            if (fromCurrency == "BRL" && toCurrency == "PEN")
            {
                rate = 0.25m; // Tasa de cambio de BRL a PEN
            }
            else if (fromCurrency == "PEN" && toCurrency == "BRL")
            {
                rate = 4.00m; // Tasa de cambio de PEN a BRL
            }
            else if (fromCurrency == "USD" && toCurrency == "BRL")
            {
                rate = 5.00m; // Tasa de cambio de USD a BRL
            }
            else if (fromCurrency == "BRL" && toCurrency == "USD")
            {
                rate = 0.20m; // Tasa de cambio de BRL a USD
            }
            else if (fromCurrency == "USD" && toCurrency == "PEN")
            {
                rate = 3.70m; // Tasa de cambio de USD a PEN
            }
            else if (fromCurrency == "PEN" && toCurrency == "USD")
            {
                rate = 0.27m; // Tasa de cambio de PEN a USD
            }

            decimal result = amount * rate;

            // Redirigir a la acción de boleta con los resultados de la conversión
            return RedirectToAction("Boleta", new { amount = amount, result = result });
        }

        // Acción para mostrar el resultado de la conversión y solicitar datos personales
        public IActionResult Boleta(decimal amount, decimal result)
        {
            // Pasar los valores de la cantidad y el resultado de la conversión a la vista
            ViewBag.Amount = amount;
            ViewBag.Result = result;

            return View();
        }

        // Acción para procesar los datos de la boleta y generar el recibo
        [HttpPost]
        public IActionResult GenerateReceipt(string name, string email, decimal amount, decimal result)
        {
            // Guardar los datos de la boleta (nombre, correo, cantidad y resultado de la conversión)
            ViewBag.Name = name;
            ViewBag.Email = email;
            ViewBag.Amount = amount;
            ViewBag.Result = result;

            // Retornar la vista del recibo (boleta final)
            return View("Receipt");
        }

        // Acción para mostrar el recibo generado
        public IActionResult Receipt()
        {
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
