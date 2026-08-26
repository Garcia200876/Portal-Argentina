using Microsoft.AspNetCore.Mvc;
using PortalArgentina.Models;

namespace PortalArgentina.Controllers
{
    public class MapaController : Controller
    {
        public IActionResult Index(string? destino = null)
        {
            if (HttpContext.Session.GetString("UsuarioNome") == null) return RedirectToAction("Index", "Login");
            var destinos = new List<Destino>
            {
                new() { Id = "buenos-aires", Nome = "Buenos Aires", Regiao = "Centro-Leste", Descricao = "Capital argentina, famosa pela Casa Rosada, pelo Obelisco, pelo tango e pela arquitetura europeia.", Latitude = -34.6037, Longitude = -58.3816, Icone = "🏙️" },
                new() { Id = "iguazu", Nome = "Cataratas do Iguaçu", Regiao = "Misiones", Descricao = "Conjunto de quedas-d’água espetacular no Parque Nacional Iguazú, na fronteira com o Brasil.", Latitude = -25.6953, Longitude = -54.4367, Icone = "💧" },
                new() { Id = "mendoza", Nome = "Mendoza", Regiao = "Cuyo", Descricao = "Destino conhecido pelos vinhedos, pela gastronomia e pela proximidade com os Andes.", Latitude = -32.8895, Longitude = -68.8458, Icone = "🍇" },
                new() { Id = "bariloche", Nome = "Bariloche", Regiao = "Patagônia", Descricao = "Cidade cercada por lagos e montanhas, muito procurada no inverno e na temporada de neve.", Latitude = -41.1335, Longitude = -71.3103, Icone = "🏔️" },
                new() { Id = "perito-moreno", Nome = "Glaciar Perito Moreno", Regiao = "Patagônia", Descricao = "Uma das paisagens glaciais mais famosas da Argentina, no Parque Nacional Los Glaciares.", Latitude = -50.4960, Longitude = -73.0476, Icone = "🧊" },
                new() { Id = "ushuaia", Nome = "Ushuaia", Regiao = "Terra do Fogo", Descricao = "Cidade no extremo sul do país, conhecida como porta de entrada para paisagens da Terra do Fogo.", Latitude = -54.8019, Longitude = -68.3030, Icone = "🧭" },
                new() { Id = "aconcagua", Nome = "Aconcágua", Regiao = "Mendoza", Descricao = "Maior montanha das Américas, localizada na Cordilheira dos Andes.", Latitude = -32.6532, Longitude = -70.0109, Icone = "⛰️" },
                new() { Id = "salta", Nome = "Salta", Regiao = "Noroeste", Descricao = "Cidade de arquitetura colonial e porta de entrada para paisagens montanhosas do noroeste argentino.", Latitude = -24.7821, Longitude = -65.4232, Icone = "🌄" }
            };

            ViewBag.DestinoInicial = destino;
            return View(destinos);
        }
    }
}
