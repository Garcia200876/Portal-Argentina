using Microsoft.AspNetCore.Mvc;
using PortalArgentina.Models;
using PortalArgentina.Services;

namespace PortalArgentina.Controllers
{
    public class CadastroController : Controller
    {
        private readonly UsuarioService _service;

        public CadastroController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index() => View(new Usuario());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);

            usuario.Email = usuario.Email.Trim().ToLowerInvariant();

            if (_service.EmailExiste(usuario.Email))
            {
                ModelState.AddModelError(nameof(usuario.Email), "Este e-mail já está cadastrado. Tente outro ou faça login.");
                return View(usuario);
            }

            bool sucesso = _service.Cadastrar(usuario);

            if (sucesso)
            {
                TempData["Sucesso"] = "Conta criada com sucesso! Agora você já pode entrar no Portal.";
                return RedirectToAction("Index", "Login");
            }

            ModelState.AddModelError(string.Empty, "Não foi possível criar sua conta agora. Verifique os dados e tente novamente.");
            return View(usuario);
        }
    }
}
