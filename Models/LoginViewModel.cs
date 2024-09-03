using System.ComponentModel.DataAnnotations;

namespace BiletPortal.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        public string UserName { get; set; }

        [Display(Name = "Parola")]
        [Required(ErrorMessage = "Parola Boş Bırakılamaz.")]
        public string Password { get; set; }
    }
}
