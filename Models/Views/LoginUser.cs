using System.ComponentModel.DataAnnotations;

namespace BELTEXAM.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Please enter your email.")]
        [Display(Name = "Email: ")]
        public string LogEmail {get;set;}

        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string LogPassword {get;set;}
    }
}