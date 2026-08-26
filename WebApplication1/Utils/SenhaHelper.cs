using System.Security.Cryptography;
using System.Text;

namespace PortalArgentina.Utils
{
    /// <summary>
    /// Hash seguro de senhas com PBKDF2. Também mantém compatibilidade
    /// com os hashes SHA-256 antigos para que usuários já cadastrados
    /// não percam o acesso ao atualizar o projeto.
    /// </summary>
    public static class SenhaHelper
    {
        private const int Iteracoes = 600_000;
        private const int TamanhoSalt = 16;
        private const int TamanhoHash = 32;

        public static string GerarHash(string senha)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(senha);

            byte[] salt = RandomNumberGenerator.GetBytes(TamanhoSalt);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                senha,
                salt,
                Iteracoes,
                HashAlgorithmName.SHA256,
                TamanhoHash);

            return $"PBKDF2${Iteracoes}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        public static bool Verificar(string senhaDigitada, string hashSalvo)
        {
            if (string.IsNullOrWhiteSpace(senhaDigitada) || string.IsNullOrWhiteSpace(hashSalvo))
                return false;

            // Formato novo: PBKDF2$iteracoes$salt$hash
            if (hashSalvo.StartsWith("PBKDF2$", StringComparison.Ordinal))
            {
                string[] partes = hashSalvo.Split('$');

                if (partes.Length != 4 ||
                    !int.TryParse(partes[1], out int iteracoes) ||
                    iteracoes <= 0)
                    return false;

                try
                {
                    byte[] salt = Convert.FromBase64String(partes[2]);
                    byte[] hashEsperado = Convert.FromBase64String(partes[3]);
                    byte[] hashDigitado = Rfc2898DeriveBytes.Pbkdf2(
                        senhaDigitada,
                        salt,
                        iteracoes,
                        HashAlgorithmName.SHA256,
                        hashEsperado.Length);

                    return CryptographicOperations.FixedTimeEquals(hashDigitado, hashEsperado);
                }
                catch (FormatException)
                {
                    return false;
                }
            }

            // Compatibilidade com o projeto anterior, que utilizava SHA-256.
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(senhaDigitada));
            string hashAntigo = Convert.ToHexString(bytes);

            return string.Equals(hashAntigo, hashSalvo, StringComparison.OrdinalIgnoreCase);
        }

        public static bool PrecisaMigrar(string hashSalvo) =>
            !string.IsNullOrWhiteSpace(hashSalvo) &&
            !hashSalvo.StartsWith("PBKDF2$", StringComparison.Ordinal);
    }
}
