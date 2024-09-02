using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiletPortal.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Etkinlik Adı")]
        [Required(ErrorMessage = "Etkinlik adı boş bırakılamaz.")]
        public string? ProductName { get; set; } = string.Empty;
        
        [Display(Name = "Etkinlik Kodu")]
        [Required(ErrorMessage = "Etkinlik kodunu eklemelisiniz.")]
        public int? ProductCode { get; set; }
       
        [Display(Name = "Açıklama")]
        [Required(ErrorMessage  = "Etkinlik açıklaması eklemelisiniz.")]
        public string? ProductDescription { get; set; } = string.Empty ;
       
        [Display(Name = "Resim")]
        [Required(ErrorMessage ="Etkinlik resmini yüklemeniz gerekiyor.")]
        public string? ProductPicture { get; set; }
        
        [Display(Name = "Etkinlik Ücreti")]
        [Required(ErrorMessage = "Etkinlik ücretini girmeniz gerekiyor.")]
        public decimal? ProductPrice { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Kategori Seçiniz.")]
        public int? CategoryId { get; set; }
   
        virtual public Category Category { get; set; }
        [NotMapped]
        public IFormFile? ImageUpload { get; set; }




    }
}
