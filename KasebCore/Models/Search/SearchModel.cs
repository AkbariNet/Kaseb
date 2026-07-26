using KasebCore.Models.Element;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace KasebCore.Models.Search
{
    public enum SortAdBy
    {
        Newest,
        Cheapest,
        MostExpensive,
        Heaviest,
        Lightest
    }
    public class SearchModel()
    {
        public static AgriculturalProductsSearchModel MainSearchModel = new  AgriculturalProductsSearchModel();

    }

    public class AgriculturalProductsSearchModel : ISearchElement
    {

        [JsonPropertyName("category")]
        public Category? Category { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        public Cities? City { get; set; } 

        [JsonPropertyName("minPrice")]
        public decimal? MinPrice { get; set; } 

        [JsonPropertyName("maxPrice")]
        public decimal? MaxPrice { get; set; } 

        [JsonPropertyName("isUrgent")]
        public bool IsUrgent { get; set; }

        [JsonPropertyName("minAreaValue")]
        public decimal? MinAreaValue { get; set; } 

        [JsonPropertyName("maxAreaValue")]
        public decimal? MaxAreaValue { get; set; } 

        [JsonPropertyName("isNonCash")]
        public bool IsNonCash { get; set; }
        public int MaxAdsValue { get; set; } = 10;
        public int LastAdID { get; set; }

    }

    public  interface ISearchElement
    {


        [JsonPropertyName("category")]
        public Category? Category { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; } 

        [JsonPropertyName("city")]
        public Cities? City { get; set; }

        [JsonPropertyName("minPrice")]
        public decimal? MinPrice { get; set; } 

        [JsonPropertyName("maxPrice")]
        public decimal? MaxPrice { get; set; } 

        [JsonPropertyName("isUrgent")]
        public bool IsUrgent { get; set; }

        [JsonPropertyName("minAreaValue")]
        public decimal? MinAreaValue { get; set; } 

        [JsonPropertyName("maxAreaValue")]
        public decimal? MaxAreaValue { get; set; } 

        [JsonPropertyName("isNonCash")]
        public bool IsNonCash { get; set; }
        public int MaxAdsValue { get; set; }
        public int LastAdID { get; set; }

    }
   

}
