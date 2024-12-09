using System.ComponentModel.DataAnnotations;

namespace bookifyWEBApi.ImportModels
{
    public class LoginRequestIm
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        required public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        required public string Password { get; set; }
    }
}
