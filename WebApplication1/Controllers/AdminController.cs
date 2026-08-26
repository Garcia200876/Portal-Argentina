using Microsoft.AspNetCore.Mvc;
using PortalArgentina.Services;

namespace PortalArgentina.Controllers
{
    public class AdminController : Controller
    {
        private readonly UsuarioService _service;

        public AdminController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioNome") == null)
                return RedirectToAction("Index", "Login");

            if (HttpContext.Session.GetString("UsuarioAdmin") != "true")
                return RedirectToAction("Index", "Home");

            return View(_service.ObterTodos());
        }
    }
}
