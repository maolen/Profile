using Services.Abstract;
using static BCrypt.Net.BCrypt;

namespace Profile.Services
{
    public class BcryptHasher : ICryptoService
    {
        public string EncryptPassword(string password)
        {
            return HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordCanditade)
        {
            return Verify(passwordCanditade, password);
        }
    }
}