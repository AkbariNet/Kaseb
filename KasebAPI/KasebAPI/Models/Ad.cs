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

        public decimal AreaValue { get; set; }

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
        house,
        apartment,
        Rent,
        Worn_texture,
        Land,


    }

    public enum Cities
    {
        IsNull,
        Yerevan,
        Gyumri,
        Vanadzor,
        Abovyan,
        Vagharshapat,
        Hrazdan,
        Kapan,
        Armavir,
        Artashat,
        Ijevan,
        Gavar,
        Goris,
        Charentsavan,
        Masis,
        Ashtarak,
        Sevan,
        Dilijan,
        Spitak,
        Sisian,
        Stepanavan,
        Martuni,
        Vardenis,
        Yeghvard,
        Byureghavan,
        NorHachn,
        Aparan,
        Berd,
        Tashir,
        Alaverdi,
        Noyemberyan,
        Jermuk,
        Chambarak,
        Metsamor,
        Vedi,
        Maralik,
        Talin,
        Tumanyan,
        Meghri,
        Agarak,
        Kajaran,
        Dastakert,
        Shamlugh,
        Ayrum,
        Tsaghkadzor,
        Ararat



    }
}