using Microsoft.AspNetCore.Mvc;

namespace PortalArgentina.Controllers
{
    public class ClubeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioNome") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}