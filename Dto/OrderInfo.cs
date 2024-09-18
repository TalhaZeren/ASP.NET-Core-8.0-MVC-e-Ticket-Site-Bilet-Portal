using BiletPortal.Models;

namespace BiletPortal.Dto
{
    public class OrderInfo
    {

        public int? ProductId { get; set; }
        public Products? Products { get; set; }
        public int? UserId { get; set; }
        public AppUser? User { get; set; }
        public int? hallId { get; set; }
        public HallInfo HallInfo { get; set; }

    }
}
