using bookifyWEBApi.ImportModels;
using Logic.Entities;

namespace bookifyWEBApi.ExtensionsMethods
{
    public static class UserEm
    {
        public static User ToUser(this RegistrationRequestIm userIM)
        {
            User user = new User(userIM.FirstName, userIM.LastName, userIM.Email, userIM.Password);
            return user;
        }
    }
}
