using Domain;
using Profile.DataAccess;
using Services.Abstract;
using System.Linq;
using System.Text.RegularExpressions;

namespace Profile.Services
{
    public class AuthService : IAuth
    {
        private readonly Context context;
        private BcryptHasher bcryptHasher = new BcryptHasher();

        public AuthService(Context context)
        {
            this.context = context;
        }

        public bool IsValidEmail(string email)
        {
            string emailFormat = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            return Regex.IsMatch(email, emailFormat);
        }

        private bool Authentication(string email)
        {
            if (context.Users.SingleOrDefault(x => x.Email == email) is null)
            {
                return false;
            }
            return true;
        }

        public bool SignUp(string fullName, string email, string password)
        {
            if (Authentication(email) || !IsValidEmail(email)) return false;
            var user = new User
            {
                Email = email,
                Password = bcryptHasher.EncryptPassword(password),
                FullName = fullName
            };

            context.Add(user);
            context.SaveChanges();
            return true;
        }

        public User SignIn(string email, string password)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;
            return user;
        }
    }
}
