using KasebAPI.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace KasebAPI.Dtos;

/// <summary>
/// مدل ورودی برای ایجاد آگهی.
/// </summary>
public class CreateAdDto
{
    [Required, MaxLength(50)]
    public string Title { get; set; } = default!;

    [MaxLength(1000)]
    public string Content { get; set; } = default!;

    [Required, MaxLength(50)]
    public string Author { get; set; } = default!;

    [Required, MaxLength(50)]
    public string City { get; set; } = default!;

    [Required, MaxLength(20)]
    public string Phone { get; set; } = default!;

    [Required, MaxLength(50)]
    [Range(typeof(decimal), "0", "79228162514264337593543950335")] // مقداردهی محدوداً
    public string Price { get; set; } = default!;   // مقدار به‌صورت string ارسال می‌شود

    public DateTime? Date { get; set; }

    public Category Category { get; set; }

    public bool IsUrgent { get; set; }
    public bool NonCash { get; set; }
    public bool SomeOfCashMostPayed { get; set; }

    public int MonthForNonCash { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public int InventoryGuarantee { get; set; }

    [MaxLength(50)]
    public string ValueOfWeighKG { get; set; } = default!;

    [MaxLength(50)]
    public string ValueOfTag1 { get; set; } = default!;
    [MaxLength(50)]
    public string ValueOfTag2 { get; set; } = default!;

    public List<IFormFile>? Images { get; set; }           // تصاویر کاربر
}
