namespace PortalArgentina.Models
{
    public class Receita
    {
        public int Id { get; set; }

        public string Nome { get; set; } = "";

        public string Imagem { get; set; } = "";

        public string Tempo { get; set; } = "";

        public string Dificuldade { get; set; } = "";

        public List<string> Ingredientes { get; set; } = new();

        public List<string> Preparo { get; set; } = new();
    }
}