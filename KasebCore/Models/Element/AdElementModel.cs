using System.ComponentModel;
using System.Text.Json.Serialization;

namespace KasebCore.Models.Element
{
    public class AdElementModel
    {
        [JsonPropertyName("author")]
        public string Author { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("category")]
        public Category? Category { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;
        public Cities? _cities;
        public Cities? Cities
        {
            get { return _cities; }

            set
            {
                _cities = value;
                City = _cities switch
                {
                    KasebCore.Models.Element.Cities.IsNull => "هنوز انتخاب نشده",
                    KasebCore.Models.Element.Cities.Barfejin => "برفجین",
                    KasebCore.Models.Element.Cities.Toejin => "توئجین",
                    KasebCore.Models.Element.Cities.Muejin => "موئجین",
                    KasebCore.Models.Element.Cities.Selulan => "سلولان",
                    KasebCore.Models.Element.Cities.HeydareBalaShahr => "حیدره بالای شهر",
                    KasebCore.Models.Element.Cities.Maryanaj => "مریانج",
                    KasebCore.Models.Element.Cities.Bahar => "بهار",
                    _  => "تعریف نشده",
                };
            }
        }

        [JsonPropertyName("images")]
        public List<AdImage>? Images { get; set; } = new();

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public string Price { get; set; } = string.Empty;

        [JsonPropertyName("isUrgent")]
        public bool IsUrgent { get; set; }

        [JsonPropertyName("isUrgentRequest")]
        public bool IsUrgentRequest { get; set; }

        [JsonPropertyName("valueOfWeighKG")]
        public string ValueOfWeighKG { get; set; } = string.Empty;

        [JsonPropertyName("valueOfTag1")]
        public string ValueOfTag1 { get; set; } = string.Empty;

        [JsonPropertyName("valueOfTag2")]
        public string ValueOfTag2 { get; set; } = string.Empty;

        [JsonPropertyName("nonCash")]
        public bool NonCash { get; set; }

        [JsonPropertyName("someOfCashMostPayed")]
        public bool SomeOfCashMostPayed { get; set; }

        [JsonPropertyName("monthForNonCash")]
        public int MonthForNonCash { get; set; }  // حداکثر 120

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("inventoryGuarantee")]
        public int InventoryGuarantee { get; set; }  // 1 تا 30 }


        [JsonPropertyName("ImagePaths")]
        public List<string> ImagePaths { get; set; } = new();

        public List<string> ImageLinks { get; set; } = new();

        public string MainImageLink { get; set; } = string.Empty;

        public string ValueSummery
        {
            get => KasebCore.Services.Combining.Combine.CombineDateAndCity(Date,City); 
        }

        public AdElementModel()
        {

        }


        public bool IsShowWeighKG
        {
            get
            {

                if (ValueOfWeighKG != null && ValueOfWeighKG != string.Empty
                    && ValueOfWeighKG != "")
                {
                    return true;
                }
                else
                    return false;

            }
        }


        //Propery For Tag1
        public bool IsShowTag1
        {
            get
            {

                if (ValueOfTag1 != null && ValueOfTag1 != string.Empty
                    && ValueOfTag1 != "")
                {
                    return true;
                }
                else
                    return false;

            }
        }

        //Propery For Tag2
        public bool IsShowTag2
        {
            get
            {

                if (ValueOfTag2 != null && ValueOfTag2 != string.Empty
                    && ValueOfTag2 != "")
                {
                    return true;
                }
                else
                    return false;

            }
        }

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
