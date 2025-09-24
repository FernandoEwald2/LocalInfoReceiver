using Api.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace Api.Util
{
    public class Criptografia
    {
        public static void CriarHashSalt(string str, out byte[] hash, out byte[] salt)
        {
            if (str == null)
                throw new LocalException(ExceptionEnum.BadRequest,"");
            if (string.IsNullOrWhiteSpace(str))
                throw new LocalException(ExceptionEnum.BadRequest, "");

            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(str));
            }

            var salti = BitConverter.ToString(salt).Replace("-", "");
            var _hashi = BitConverter.ToString(hash).Replace("-", "");
        }

        public static bool VerificarHashSalt(string str, byte[] hash, byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(str)) throw new LocalException(ExceptionEnum.Unauthorized, "Informe uma Senha");
            if (hash == null) throw new LocalException(ExceptionEnum.Unauthorized, "Não há uma senha cadastrada.");
            if (hash.Length != 64) throw new LocalException(ExceptionEnum.InternalServerError, "Senha inválida.");
            if (salt.Length != 128) throw new LocalException(ExceptionEnum.InternalServerError, "Senha inválida.");

            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(str));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != hash[i]) return false;
                }
            }

            return true;
        }
    }

}
