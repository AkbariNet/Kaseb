
using System.Text.Json.Serialization;

namespace KasebAPI.Models.Search
{
    public enum SortAdBy
    {
        Newest,
        Cheapest,
        MostExpensive,
        Heaviest,
        Lightest
    }
    public class AgriculturalProductsSearchModel :ISearchElement
    {



        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        public string City { get; set; } = string.Empty;

        [JsonPropertyName("minPrice")]
        public string MinPrice { get; set; } = string.Empty;

        [JsonPropertyName("maxPrice")]
        public string MaxPrice { get; set; } = string.Empty;

        [JsonPropertyName("isUrgent")]
        public bool IsUrgent { get; set; }

        [JsonPropertyName("minValueOfWeighKG")]
        public string MinValueOfWeighKG { get; set; } = string.Empty;

        [JsonPropertyName("maxValueOfWeighKG")]
        public string MaxValueOfWeighKG { get; set; } = string.Empty;

        [JsonPropertyName("isNonCash")]
        public bool IsNonCash { get; set; }

    }




    public interface ISearchElement
    {


        [JsonPropertyName("category")]
        public string? Category { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } 

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("minPrice")]
        public string MinPrice { get; set; } 

        [JsonPropertyName("maxPrice")]
        public string MaxPrice { get; set; } 

        [JsonPropertyName("isUrgent")]
        public bool IsUrgent { get; set; }

        [JsonPropertyName("minValueOfWeighKG")]
        public string MinValueOfWeighKG { get; set; } 

        [JsonPropertyName("maxValueOfWeighKG")]
        public string MaxValueOfWeighKG { get; set; } 

        [JsonPropertyName("isNonCash")]
        public bool IsNonCash { get; set; }
    }
   

}
