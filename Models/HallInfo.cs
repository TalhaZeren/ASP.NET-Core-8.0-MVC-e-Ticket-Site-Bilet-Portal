using System.ComponentModel.DataAnnotations;

namespace BiletPortal.Models
{
    public class HallInfo
    {

        [Key]
        public int Hallid { get; set; }
        public string? HallName { get; set; }
        public string? City { get; set; }
        public string? LocationInformation { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public int ProductId { get; set; }
        public Products? Products { get; set; }
    }
}
