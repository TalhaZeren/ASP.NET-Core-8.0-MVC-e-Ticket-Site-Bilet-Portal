using BiletPortal.Models;

namespace BiletPortal.Dto
{
    public class CardViewModel
    {
        public List<CardItem> CardItems { get; set; }   
        public decimal GrandTotal {  get; set; }
    }
}
