using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiletPortal.Models
{
    public class HallInfo
    {

        [Key]
        public int Hallid { get; set; }
        [Display(Name = "Salon Adı")]
        [Required(ErrorMessage = "Salon adı boş bırakılamaz.")]
        public string? HallName { get; set; }
        [Display(Name = "Şehir")]
        [Required(ErrorMessage = "Şehir Eklemelisiniz.")]
        public string? City { get; set; }
        [Display(Name = "Konum")]
        [Required(ErrorMessage = "Salonun tam konumunu lütfen ekleyiniz.")]
        public string? LocationInformation { get; set; }
        [Display(Name = "Tarih")]
        [Required(ErrorMessage = "Tarih bilgisi eklenmeden tamamlanamaz.")]
        public string? Date { get; set; }
        [Display(Name = "Zaman")]
        [Required(ErrorMessage = "Salonun etkinlik için ayrılmış olan saatini girmelisiniz.")]
        public string? Time { get; set; }
        [Display(Name = "Salon Resmi")]
        [Required(ErrorMessage = "Etkinlik resmi boş bırakılamaz.")]
        public string? HallPicture { get; set; }
        [NotMapped]
        public IFormFile? ImageUpload { get; set; }
        [Display(Name = "Etkinlik Adı")]
        [Required(ErrorMessage = "Etkinlik adı boş bırakılamaz.")]
        public int ProductId { get; set; }
        [Display(Name = "Etkinlik Adı")]
        [Required(ErrorMessage = "Etkinlik adı boş bırakılamaz.")]
        public Products? Products { get; set; }
  
        

    }
}
