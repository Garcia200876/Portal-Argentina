using Microsoft.AspNetCore.Mvc;
using PortalArgentina.Models;
using System.Diagnostics;

namespace PortalArgentina.Controllers
{
    // Controller responsável pela página inicial do site.
    public class HomeController : Controller
    {
        // Método executado quando o usuário acessa "/Home"
        // ou simplesmente a página inicial.
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Usada por app.UseExceptionHandler("/Home/Error") em produção.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}