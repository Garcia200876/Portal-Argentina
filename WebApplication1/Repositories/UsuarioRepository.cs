using MySql.Data.MySqlClient;
using PortalArgentina.Data;
using PortalArgentina.Models;

namespace PortalArgentina.Repositories
{
    public class UsuarioRepository
    {
        private readonly Conexao _conexao;

        public UsuarioRepository(Conexao conexao)
        {
            _conexao = conexao;
        }

        public bool TestarConexao()
        {
            try
            {
                using MySqlConnection conexao = _conexao.ObterConexao();
                conexao.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EmailExiste(string email)
        {
            using MySqlConnection conexao = _conexao.ObterConexao();
            conexao.Open();
            const string sql = "SELECT 1 FROM usuarios WHERE email = @email LIMIT 1";
            using MySqlCommand cmd = new(sql, conexao);
            cmd.Parameters.AddWithValue("@email", email.Trim().ToLowerInvariant());
            return cmd.ExecuteScalar() is not null;
        }

        public bool Cadastrar(Usuario usuario)
        {
            try
            {
                using MySqlConnection conexao = _conexao.ObterConexao();
                conexao.Open();

                const string sql = @"INSERT INTO usuarios
                    (nome, email, senha, administrador, pontosQuiz)
                    VALUES (@nome, @email, @senha, @admin, @pontos);";

                using MySqlCommand cmd = new(sql, conexao);
                cmd.Parameters.AddWithValue("@nome", usuario.Nome.Trim());
                cmd.Parameters.AddWithValue("@email", usuario.Email.Trim().ToLowerInvariant());
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@admin", usuario.Administrador);
                cmd.Parameters.AddWithValue("@pontos", usuario.PontosQuiz);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public Usuario? BuscarPorEmail(string email)
        {
            using MySqlConnection conexao = _conexao.ObterConexao();
            conexao.Open();

            const string sql = "SELECT id, nome, email, senha, administrador, pontosQuiz FROM usuarios WHERE email = @email LIMIT 1";

            using MySqlCommand cmd = new(sql, conexao);
            cmd.Parameters.AddWithValue("@email", email.Trim().ToLowerInvariant());

            using MySqlDataReader reader = cmd.ExecuteReader();

            return reader.Read() ? LerUsuario(reader) : null;
        }

        public List<Usuario> ObterRanking(int limite = 10)
        {
            limite = Math.Clamp(limite, 1, 100);
            List<Usuario> ranking = new();

            using MySqlConnection conexao = _conexao.ObterConexao();
            conexao.Open();

            const string sql = @"SELECT id, nome, email, senha, administrador, pontosQuiz
                                 FROM usuarios
                                 ORDER BY pontosQuiz DESC, nome ASC
                                 LIMIT @limite";

            using MySqlCommand cmd = new(sql, conexao);
            cmd.Parameters.AddWithValue("@limite", limite);

            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                ranking.Add(LerUsuario(reader));

            return ranking;
        }

        public List<Usuario> ObterTodos()
        {
            List<Usuario> usuarios = new();

            using MySqlConnection conexao = _conexao.ObterConexao();
            conexao.Open();

            const string sql = @"SELECT id, nome, email, senha, administrador, pontosQuiz
                                 FROM usuarios ORDER BY id DESC";

            using MySqlCommand cmd = new(sql, conexao);
            using MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                usuarios.Add(LerUsuario(reader));

            return usuarios;
        }

        public void AtualizarPontuacao(string email, int pontos)
        {
            pontos = Math.Clamp(pontos, 0, 10);

            using MySqlConnection conexao = _conexao.ObterConexao();
            conexao.Open();

            const string sql = @"UPDATE usuarios
                                 SET pontosQuiz = CASE
                                     WHEN pontosQuiz < @pontos THEN @pontos
                                     ELSE pontosQuiz
                                 END
                                 WHERE email = @email;";

            using MySqlCommand cmd = new(sql, conexao);
            cmd.Parameters.AddWithValue("@pontos", pontos);
            cmd.Parameters.AddWithValue("@email", email.Trim().ToLowerInvariant());
            cmd.ExecuteNonQuery();
        }

        public void AtualizarSenha(string email, string novoHash)
        {
            using MySqlConnection conexao = _conexao.ObterConexao();
            conexao.Open();

            const string sql = "UPDATE usuarios SET senha = @senha WHERE email = @email";

            using MySqlCommand cmd = new(sql, conexao);
            cmd.Parameters.AddWithValue("@senha", novoHash);
            cmd.Parameters.AddWithValue("@email", email.Trim().ToLowerInvariant());
            cmd.ExecuteNonQuery();
        }

        private static Usuario LerUsuario(MySqlDataReader reader)
        {
            return new Usuario
            {
                Id = Convert.ToInt32(reader["id"]),
                Nome = reader["nome"].ToString() ?? "",
                Email = reader["email"].ToString() ?? "",
                Senha = reader["senha"].ToString() ?? "",
                Administrador = Convert.ToBoolean(reader["administrador"]),
                PontosQuiz = Convert.ToInt32(reader["pontosQuiz"])
            };
        }
    }
}
