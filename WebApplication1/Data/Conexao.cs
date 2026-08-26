using MySql.Data.MySqlClient;

namespace PortalArgentina.Data
{
    public class Conexao
    {
        private readonly string _connectionString;

        public Conexao(IConfiguration configuration)
        {
            _connectionString = Environment.GetEnvironmentVariable("PORTALARGENTINA_CONNECTION_STRING")
                ?? configuration.GetConnectionString("PortalArgentina")
                ?? throw new InvalidOperationException(
                    "Connection string 'PortalArgentina' não encontrada. Configure appsettings.json ou PORTALARGENTINA_CONNECTION_STRING.");
        }

        public MySqlConnection ObterConexao() => new(_connectionString);
    }
}
