using Microsoft.AspNetCore.Mvc;

namespace PortalArgentina.Controllers
{
    public class CulturaController : Controller
    {
        // Exibe a página Cultura
        public IActionResult Index()
        {
            return View();
        }
    }
}