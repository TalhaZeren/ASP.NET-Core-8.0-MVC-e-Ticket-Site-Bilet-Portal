using System.ComponentModel.DataAnnotations;

namespace BiletPortal.Dto
{
    public class AppUserRegisterDto
    {
        [Display(Name ="Adınız")]
        [Required(ErrorMessage ="'Ad' bölümü boş bırakılamaz.")]
        public string FirstName { get; set; }

        [Display(Name = "Soyadınız")]
        [Required(ErrorMessage = "'Soyad' bölümü boş bırakılamaz.")]
        public string LastName { get; set; }

        [Display(Name = "Kullanıcı Adınız")]
        [Required(ErrorMessage = "'Kullanıcı Adı' bölümü boş bırakılamaz.")]
        public string UserName { get; set; }

        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "'Şehir' bölümü boş bırakılamaz.")]
        public string City { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "'Email' bölümü boş bırakılamaz.")]
        public string Email { get; set; }

        [Display(Name = "Telefon Numarası")]
        [Required(ErrorMessage = "'Telefon Numarası' bölümü boş bırakılamaz.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "'Şifre' bölümü boş bırakılamaz.")]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }                     

    }
}
