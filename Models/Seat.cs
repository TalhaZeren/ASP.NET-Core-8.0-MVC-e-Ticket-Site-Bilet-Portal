using System.ComponentModel.DataAnnotations;

namespace BiletPortal.Models
{
    public class Seat
    {
        [Key]
        public int SeatId { get; set; }
        public string seatIdNumber { get; set; }
        public string SeatNumber { get; set; }
        public bool IsBooked { get; set; }

    }
}
