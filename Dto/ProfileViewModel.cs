using BiletPortal.Models;

namespace BiletPortal.Dto
{
    public class ProfileViewModel
    {
        public AppUser AppUser { get; set; }
        public List<SelectSeat> Tickets { get; set; }

    }
}
