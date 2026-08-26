using Microsoft.AspNetCore.Mvc;
using PortalArgentina.Models;

namespace PortalArgentina.Controllers
{
    public class ReceitasController : Controller
    {
        private static readonly List<Receita> Receitas = new()
        {
            new Receita
            {
                Id = 1, Nome = "Empanadas", Imagem = "/images/receitas/empanadas.jpg",
                Tempo = "50 minutos", Dificuldade = "Média",
                Ingredientes = new() { "500 g de farinha", "300 g de carne moída", "1 cebola", "Azeitonas", "2 ovos cozidos" },
                Preparo = new() { "Prepare a massa.", "Refogue a carne com a cebola e os temperos.", "Recheie e feche as empanadas.", "Asse a 200 °C por aproximadamente 30 minutos." }
            },
            new Receita
            {
                Id = 2, Nome = "Choripán", Imagem = "/images/receitas/choripan.jpg",
                Tempo = "20 minutos", Dificuldade = "Fácil",
                Ingredientes = new() { "Linguiça", "Pão", "Molho chimichurri" },
                Preparo = new() { "Grelhe a linguiça.", "Abra o pão sem separar completamente as duas partes.", "Coloque a linguiça no pão.", "Finalize com chimichurri." }
            },
            new Receita
            {
                Id = 3, Nome = "Milanesa", Imagem = "/images/receitas/milanesa.jpg",
                Tempo = "40 minutos", Dificuldade = "Fácil",
                Ingredientes = new() { "Bifes finos", "Farinha de rosca", "Ovos", "Sal", "Pimenta" },
                Preparo = new() { "Tempere os bifes.", "Passe cada bife nos ovos batidos.", "Empane com farinha de rosca.", "Frite ou asse até dourar." }
            },
            new Receita
            {
                Id = 4, Nome = "Alfajor", Imagem = "/images/receitas/alfajor.jpg",
                Tempo = "1 hora", Dificuldade = "Média",
                Ingredientes = new() { "Farinha", "Amido de milho", "Doce de leite", "Chocolate" },
                Preparo = new() { "Prepare e abra a massa.", "Corte os discos e asse.", "Una os biscoitos com doce de leite.", "Cubra com chocolate, se desejar." }
            },
            new Receita
            {
                Id = 5, Nome = "Asado Argentino", Imagem = "/images/turismo/asado.jpg",
                Tempo = "2 horas", Dificuldade = "Média",
                Ingredientes = new() { "Carnes bovinas", "Sal grosso", "Carvão", "Molho chimichurri" },
                Preparo = new() { "Prepare a brasa e espere formar uma camada de carvão quente.", "Tempere a carne com sal grosso.", "Grelhe lentamente, virando quando necessário.", "Sirva com chimichurri e acompanhamentos." }
            }
        };

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioNome") == null) return RedirectToAction("Index", "Login");
            return View(Receitas);
        }

        public IActionResult Detalhes(int id)
        {
            if (HttpContext.Session.GetString("UsuarioNome") == null) return RedirectToAction("Index", "Login");
            Receita? receita = Receitas.FirstOrDefault(r => r.Id == id);
            return receita == null ? NotFound() : View(receita);
        }
    }
}
