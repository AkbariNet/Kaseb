using KasebAPI.Models;

namespace KasebAPI.Models.DTOs
{
    public class CreateAdDto
    {
        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? Author { get; set; }

        public string? City { get; set; }

        public string? Phone { get; set; }

        public decimal Price { get; set; }

        public DateTime? Date { get; set; }

        public Category Category { get; set; }

        public bool IsUrgent { get; set; }

        public bool NonCash { get; set; }

        public bool SomeOfCashMostPayed { get; set; }

        public int MonthForNonCash { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int InventoryGuarantee { get; set; }

        public decimal ValueOfWeighKG { get; set; }

        public string? ValueOfTag1 { get; set; }

        public string? ValueOfTag2 { get; set; }


        public List<IFormFile>? Images { get; set; }

    }
}