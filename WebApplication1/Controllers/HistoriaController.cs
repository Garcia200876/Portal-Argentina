using Microsoft.AspNetCore.Mvc;

namespace PortalArgentina.Controllers
{
    // Responsável por todas as páginas relacionadas à História da Argentina.
    public class HistoriaController : Controller
    {
        // Exibe a página principal de História.
        public IActionResult Index()
        {
            return View();
        }
    }
}