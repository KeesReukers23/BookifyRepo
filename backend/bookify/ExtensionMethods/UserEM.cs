using bookifyWEBApi.ImportModels;
using Logic.Entities;

namespace bookifyWEBApi.ExtensionsMethods
{
    public static class UserEM
    {
        public static User ToUser(this RegistrationRequestIM userIM)
        {
            User user = new User(userIM.FirstName, userIM.LastName, userIM.Email, userIM.Password);
            return user;
        }
    }
}
