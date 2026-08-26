using Microsoft.AspNetCore.Mvc;
using PortalArgentina.Models;
using PortalArgentina.Services;

namespace PortalArgentina.Controllers
{
    public class QuizController : Controller
    {
        private readonly UsuarioService _usuarioService;

        private static readonly List<PerguntaQuiz> Perguntas = new()
        {
            new() { Pergunta = "Qual é a capital da Argentina?", Alternativas = new() { "Rosário", "Córdoba", "Buenos Aires", "Mendoza" }, RespostaCorreta = 2 },
            new() { Pergunta = "Qual é a moeda oficial da Argentina?", Alternativas = new() { "Peso Argentino", "Real", "Dólar", "Euro" }, RespostaCorreta = 0 },
            new() { Pergunta = "Qual dança é considerada símbolo da Argentina?", Alternativas = new() { "Samba", "Tango", "Flamenco", "Valsa" }, RespostaCorreta = 1 },
            new() { Pergunta = "Qual destas cidades fica na Patagônia?", Alternativas = new() { "Bariloche", "Rosário", "La Plata", "Salta" }, RespostaCorreta = 0 },
            new() { Pergunta = "Qual montanha fica na Argentina?", Alternativas = new() { "Monte Everest", "Aconcágua", "Monte Fuji", "Kilimanjaro" }, RespostaCorreta = 1 },
            new() { Pergunta = "Qual bebida é tradicional na Argentina?", Alternativas = new() { "Mate", "Café", "Chá Inglês", "Chocolate" }, RespostaCorreta = 0 },
            new() { Pergunta = "Qual oceano banha a Argentina?", Alternativas = new() { "Pacífico", "Atlântico", "Índico", "Ártico" }, RespostaCorreta = 1 },
            new() { Pergunta = "Qual é o idioma oficial predominante da Argentina?", Alternativas = new() { "Português", "Inglês", "Espanhol", "Italiano" }, RespostaCorreta = 2 },
            new() { Pergunta = "Qual famoso glaciar argentino fica na Patagônia?", Alternativas = new() { "Perito Moreno", "Upsala", "Viedma", "Spegazzini" }, RespostaCorreta = 0 },
            new() { Pergunta = "Em que continente fica a Argentina?", Alternativas = new() { "Europa", "África", "América do Sul", "Ásia" }, RespostaCorreta = 2 }
        };

        public QuizController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UsuarioNome") == null) return RedirectToAction("Index", "Login");
            return View(Perguntas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Corrigir(List<int>? respostas)
        {
            if (HttpContext.Session.GetString("UsuarioNome") == null)
                return RedirectToAction("Index", "Login");

            respostas ??= new List<int>();
            int acertos = 0;

            for (int i = 0; i < Perguntas.Count && i < respostas.Count; i++)
            {
                if (respostas[i] == Perguntas[i].RespostaCorreta)
                    acertos++;
            }

            string? email = HttpContext.Session.GetString("UsuarioEmail");
            if (!string.IsNullOrWhiteSpace(email))
                _usuarioService.AtualizarPontuacao(email, acertos);

            return View("Resultado", acertos);
        }
    }
}
