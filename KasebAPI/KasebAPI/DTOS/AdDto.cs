using KasebAPI.Models;

namespace KasebAPI.Dtos;

/// <summary>
/// خروجی برای خواندن آگهی؛ شامل تمام فیلدهای مربوط به Ad
/// و مسیرهای تصاویر (به‌صورت relative).
/// </summary>
public class AdDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Price { get; set; } = default!;
    public DateTime? Date { get; set; }
    public Category Category { get; set; }
    public bool IsUrgent { get; set; }
    public bool NonCash { get; set; }
    public bool SomeOfCashMostPayed { get; set; }
    public int MonthForNonCash { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int InventoryGuarantee { get; set; }
    public string ValueOfWeighKG { get; set; } = default!;
    public string ValueOfTag1 { get; set; } = default!;
    public string ValueOfTag2 { get; set; } = default!;
    public IEnumerable<string> Images { get; set; } = Array.Empty<string>();
}
