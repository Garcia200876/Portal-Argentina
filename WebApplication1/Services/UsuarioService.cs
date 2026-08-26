using PortalArgentina.Models;
using PortalArgentina.Repositories;
using PortalArgentina.Utils;

namespace PortalArgentina.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository;

        public UsuarioService(UsuarioRepository repository)
        {
            _repository = repository;
        }

        public bool EmailExiste(string email) => _repository.EmailExiste(email);

        public bool Cadastrar(Usuario usuario)
        {
            usuario.Nome = usuario.Nome.Trim();
            usuario.Email = usuario.Email.Trim().ToLowerInvariant();
            usuario.Administrador = false;
            usuario.PontosQuiz = 0;
            usuario.Senha = SenhaHelper.GerarHash(usuario.Senha);

            return _repository.Cadastrar(usuario);
        }

        public Usuario? Login(string email, string senha)
        {
            email = email.Trim().ToLowerInvariant();
            Usuario? usuario = _repository.BuscarPorEmail(email);

            if (usuario == null || !SenhaHelper.Verificar(senha, usuario.Senha))
                return null;

            // Migra automaticamente contas antigas do SHA-256 para PBKDF2.
            if (SenhaHelper.PrecisaMigrar(usuario.Senha))
            {
                string novoHash = SenhaHelper.GerarHash(senha);
                _repository.AtualizarSenha(usuario.Email, novoHash);
                usuario.Senha = novoHash;
            }

            return usuario;
        }

        public void AtualizarPontuacao(string email, int pontos) =>
            _repository.AtualizarPontuacao(email, pontos);

        public List<Usuario> ObterRanking(int limite = 10) =>
            _repository.ObterRanking(limite);

        public List<Usuario> ObterTodos() => _repository.ObterTodos();
    }
}
