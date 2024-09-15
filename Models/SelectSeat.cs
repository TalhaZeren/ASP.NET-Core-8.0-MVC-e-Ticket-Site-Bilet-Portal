using System.ComponentModel.DataAnnotations;

namespace BiletPortal.Models
{
    public class SelectSeat
    {
        [Key]
        public int SeatId { get; set; }
        public string seatIdNumber { get; set; }
        public string SeatNumber { get; set; }
        public bool IsBooked { get; set; }
        public int? ProductId { get; set; }
       public Products? Products { get; set; }
        public int? UserId { get; set; }
        public AppUser? User { get; set; }

    }
}
