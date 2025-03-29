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

        
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public IActionResult ChangeCurrency(string fromCurrency, string toCurrency, decimal amount)
        {
            decimal rate = 1;

            // Tasa de cambio entre BRL, PEN y USD
            if (fromCurrency == "BRL" && toCurrency == "PEN")
            {
                rate = 0.634m; // 1 BRL = 0.634 PEN (actualizada)
            }
            else if (fromCurrency == "PEN" && toCurrency == "BRL")
            {
                rate = 1.577m; // 1 PEN = 1.577 BRL (inverso de 1.00 BRL = 0.634 PEN)
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
                rate = 3.64m; // 1 USD = 3.64 PEN (actualizada)
            }
            else if (fromCurrency == "PEN" && toCurrency == "USD")
            {
                rate = 0.274m; // 1 PEN = 0.274 USD (inverso de 1 USD = 3.64 PEN)
            }

            decimal result = amount * rate;


            return RedirectToAction("Boleta", new { amount = amount, result = result });
        }

        
        public IActionResult Boleta(decimal amount, decimal result)
        {
           
            ViewBag.Amount = amount;
            ViewBag.Result = result;

            return View();
        }

       
        [HttpPost]
        public IActionResult GenerateReceipt(string name, string email, decimal amount, decimal result)
        {
            
            ViewBag.Name = name;
            ViewBag.Email = email;
            ViewBag.Amount = amount;
            ViewBag.Result = result;

            
            return View("Receipt");
        }

        
        public IActionResult Receipt()
        {
            return View();
        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
