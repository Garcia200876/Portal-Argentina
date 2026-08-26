using Microsoft.AspNetCore.Mvc;
using PortalArgentina.Services;

namespace PortalArgentina.Controllers
{
    public class RankingController : Controller
    {
        private readonly UsuarioService _service;

        public RankingController(UsuarioService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioNome") == null) return RedirectToAction("Index", "Login");
            var ranking = _service.ObterRanking(10);

            return View(ranking);
        }
    }
}
