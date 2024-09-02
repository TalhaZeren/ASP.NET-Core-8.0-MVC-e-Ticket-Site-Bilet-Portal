using BiletPortal.Models;

namespace BiletPortal.Dto
{
    public class SearchViewModel
    {

        public List<Products> Products {  get; set; }
        public List<Slider> Slider { get; set; }
        public string SearchQuery {  get; set; }
    }
}
