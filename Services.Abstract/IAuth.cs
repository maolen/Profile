using Domain;

namespace Services.Abstract
{
    public interface IAuth
    {
        bool SignUp(string fullName, string email, string password);
        User SignIn(string email, string password);
    }
}
