using System.ComponentModel.DataAnnotations;

namespace BiletPortal.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage ="Kategoriyi seçmeniz gerekiyor.")]
        public int CategoryId { get; set; }
   
        public string? CategoryName { get; set; } = string.Empty;
      
        virtual public List<Products> Products { get; set; } 

    }
}
