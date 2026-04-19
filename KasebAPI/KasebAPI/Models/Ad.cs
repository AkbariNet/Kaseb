using System.ComponentModel.DataAnnotations;

namespace KasebAPI.Models;

/// <summary>
/// نمایندگی یک آگهی.
/// </summary>
public class Ad
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string Title { get; set; } = default!;

    [MaxLength(1000)]
    public string Content { get; set; } = default!;

    [MaxLength(50)]
    public string Author { get; set; } = default!;

    [MaxLength(50)]
    public string City { get; set; } = default!;

    [MaxLength(20)]
    public string Phone { get; set; } = default!;

    [MaxLength(50)]
    public string Price { get; set; } = default!;              // به‌صورت string ذخیره می‌شود تا امکان فرمت دلخواه داشته باشد

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
    public string ValueOfWeighKG { get; set; } = default!;     // string→decimal در لایه سرویس کنونی.

    [MaxLength(50)]
    public string ValueOfTag1 { get; set; } = default!;
    [MaxLength(50)]
    public string ValueOfTag2 { get; set; } = default!;

    public List<AdImage>? Images { get; set; }                 // navigation
}

/// <summary>
/// دسته‌بندی محصولات.
/// </summary>
public enum Category
{
    IsNull, Garlic, Shallot, Walnut, Potato, Cucumber, Tomato,
    Mushroom, Almond
}

/// <summary>
/// شهرها (داده‌آیته‌سازی ساده)
/// </summary>
public enum Cities
{
    IsNull, Barfejin, Toejin, Muejin, Selulan, HeydareBalaShahr,
    Maryanaj, Bahar
}
