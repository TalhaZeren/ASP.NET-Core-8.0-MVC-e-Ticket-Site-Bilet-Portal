using System.ComponentModel.DataAnnotations;

namespace BiletPortal.Models
{
    public class Payment
    {

        [Key]
        public int PaymentId{ get; set; }

        [Required]
        [StringLength(50)]
        public string CardHolderName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string ExpirationDateMonth { get; set; }
        [Required]
        public string ExpirationDateYear { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string CVV { get; set; }

        [Required]
        public string Email { get; set; }

        public int TransactionId { get; set; } // The proccess Id coming from Paying Server.
        public bool IsSuccessful { get; set; }  // Is the payment successful?
        public string? PaymentStatus { get; set; } = string.Empty;// "Completed" , "Failed" , "Pending" 






    }
}
