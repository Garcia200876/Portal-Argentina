namespace PortalArgentina.Models
{
    public class Destino
    {
        public string Id { get; set; } = "";
        public string Nome { get; set; } = "";
        public string Regiao { get; set; } = "";
        public string Descricao { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Icone { get; set; } = "📍";
    }
}
