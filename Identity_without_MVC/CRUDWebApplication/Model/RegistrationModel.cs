using System.ComponentModel.DataAnnotations;

namespace CRUDWebApplication.Model
{
    public class RegistrationModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password don't match!")]
        public string ConfirmPassword { get; set; }
    }
}
