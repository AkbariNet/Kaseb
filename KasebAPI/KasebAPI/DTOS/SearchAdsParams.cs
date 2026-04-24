using KasebAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KasebAPI.Dtos;

/// <summary>
/// پارامترهای جستجو به شکل DTO (برای `FromQuery`).
/// </summary>
public class SearchAdsParamsDto
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

    [JsonPropertyName("isNonCash")]
    public bool IsNonCash { get; set; }

    [JsonPropertyName("minValueOfWeighKG")]
    public decimal? MinValueOfWeighKG { get; set; }

    [JsonPropertyName("maxValueOfWeighKG")]
    public decimal? MaxValueOfWeighKG { get; set; }

    // مرتب سازی
    [JsonPropertyName("sortBy")]
    public SortAdBy? SortBy { get; set; }

    // صفحه بندی
    [Range(1, int.MaxValue, ErrorMessage = "Page must be positive")]
    public int PageNumber { get; set; } = 1;

    [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
    public int PageSize { get; set; } = 10;
}

public enum SortAdBy
{
    Newest,
    Cheapest,
    MostExpensive,
    Heaviest,
    Lightest
}
