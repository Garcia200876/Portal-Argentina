using Microsoft.AspNetCore.Mvc;

namespace PortalArgentina.Controllers
{
    public class EsporteController : Controller
    {
        // Página principal de Esportes
        public IActionResult Index()
        {
            return View();
        }
    }
}