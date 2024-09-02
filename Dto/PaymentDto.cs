using System.ComponentModel.DataAnnotations;

namespace BiletPortal.Dto
{
    public class PaymentDto
    {
        [Required]
        [StringLength(50)]
        public string CardHolderName { get; set; }
        
        [Required]
        public string CardNumber { get; set; }

        public string ExpirationDateMonth { get; set; }
        public string ExpirationDateYear { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string CVV { get; set; }

        [Required]
        public string Email { get; set; }


    }
}
