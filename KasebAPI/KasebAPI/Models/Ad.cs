using System.ComponentModel.DataAnnotations;

namespace KasebAPI.Models
{
    public class Ad
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Required]
        public int Id { get; set; }


        [MaxLength(50)]
        public string? Title { get; set; }

        [MaxLength(1000)]
        public string? Content { get; set; }

        [MaxLength(50)]
        public string? Author { get; set; }

        [MaxLength(50)]
        public string? City { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(50)]
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

        [MaxLength(50)]
        public string? ValueOfTag1 { get; set; }

        [MaxLength(50)]
        public string? ValueOfTag2 { get; set; }


        // navigation property
        public List<AdImage>? Images { get; set; }

    }


    public enum Category
    {
        IsNull,
        Garlic,
        Shallot,
        Walnut,
        Potato,
        Cucumber,
        Tomato,
        Mushroom,
        Almond,
    }
    public enum Cities
    {
        IsNull,
        Barfejin,
        Toejin,
        Muejin,
        Selulan,
        HeydareBalaShahr,
        Maryanaj,
        Bahar,



    }
}