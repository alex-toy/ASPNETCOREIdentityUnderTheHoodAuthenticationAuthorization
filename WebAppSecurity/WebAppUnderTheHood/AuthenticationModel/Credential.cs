using System.ComponentModel.DataAnnotations;

namespace WebAppUnderTheHood.AuthenticationModel
{
    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Remember Me")]   
        public bool RememberMe { get; set; }
    }
}
