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
    public static class AgriculturalProductsSearchModel 
    {



        [JsonPropertyName("category")]
        public static string? Category { get; set; }

        [JsonPropertyName("title")]
        public static string Title { get; set; } = string.Empty;

        [JsonPropertyName("city")]
        public static string City { get; set; } = string.Empty;

        [JsonPropertyName("minPrice")]
        public static string MinPrice { get; set; } = string.Empty;

        [JsonPropertyName("maxPrice")]
        public static string MaxPrice { get; set; } = string.Empty;

        [JsonPropertyName("isUrgent")]
        public static bool IsUrgent { get; set; }

        [JsonPropertyName("minValueOfWeighKG")]
        public static string MinValueOfWeighKG { get; set; } = string.Empty;

        [JsonPropertyName("maxValueOfWeighKG")]
        public static string MaxValueOfWeighKG { get; set; } = string.Empty;

        [JsonPropertyName("isNonCash")]
        public static bool IsNonCash { get; set; }

    }




    public  interface ISearchElement
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
