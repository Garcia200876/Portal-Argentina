namespace PortalArgentina.Models
{
    public class PerguntaQuiz
    {
        public string Pergunta { get; set; } = "";

        public List<string> Alternativas { get; set; } = new();

        public int RespostaCorreta { get; set; }
    }
}