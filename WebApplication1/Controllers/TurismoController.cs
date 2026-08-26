using Microsoft.AspNetCore.Mvc;

namespace PortalArgentina.Controllers
{
    public class TurismoController : Controller
    {
        // Página principal de Turismo
        public IActionResult Index()
        {
            return View();
        }
    }
}