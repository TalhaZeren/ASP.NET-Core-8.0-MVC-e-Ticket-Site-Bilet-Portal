using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiletPortal.Models
{
    public class Slider
    {
        [Key]
        public int SliderId { get; set; }
        [Display(Name = "Kaydirici Adi")]
        public string? SliderName { get; set; } = string.Empty;
        [Display(Name = "Birinci Başlik")]
        public string? Header1 { get; set; } = string.Empty ;
        [Display(Name = "Ikinci Başlık")]
        public string? Header2 { get; set; } = string.Empty;
        public string? Context { get; set; } = string.Empty;
        [Display(Name = "Resim")]
        public string? SliderImage { get; set; } = string.Empty;
        [NotMapped]
       
        public IFormFile? ImageUpload { get; set; }


    }
}
