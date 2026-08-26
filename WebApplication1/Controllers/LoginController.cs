using Microsoft.AspNetCore.Mvc;
using PortalArgentina.Models;
using PortalArgentina.Services;

namespace PortalArgentina.Controllers
{
    public class LoginController : Controller
    {
        private readonly UsuarioService _service;

        public LoginController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioNome") != null)
                return RedirectToAction("Index", "Clube");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Index(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                ViewBag.Mensagem = "Informe e-mail e senha.";
                return View();
            }

            Usuario? usuario = _service.Login(email, senha);

            if (usuario != null)
            {
                HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
                HttpContext.Session.SetString("UsuarioEmail", usuario.Email);
                HttpContext.Session.SetString("UsuarioAdmin", usuario.Administrador ? "true" : "false");

                return RedirectToAction("Index", "Clube");
            }

            ViewBag.Mensagem = "E-mail ou senha inválidos.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
