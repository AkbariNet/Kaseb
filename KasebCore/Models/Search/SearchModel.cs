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

        [JsonPropertyName("minValueOfWeighKG")]
        public decimal? MinValueOfWeighKG { get; set; } 

        [JsonPropertyName("maxValueOfWeighKG")]
        public decimal? MaxValueOfWeighKG { get; set; } 

        [JsonPropertyName("isNonCash")]
        public bool IsNonCash { get; set; }

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

        [JsonPropertyName("minValueOfWeighKG")]
        public decimal? MinValueOfWeighKG { get; set; } 

        [JsonPropertyName("maxValueOfWeighKG")]
        public decimal? MaxValueOfWeighKG { get; set; } 

        [JsonPropertyName("isNonCash")]
        public bool IsNonCash { get; set; }
    }
   

}
