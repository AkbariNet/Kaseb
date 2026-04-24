namespace KasebAPI.Models;

/// <summary>
/// نماگر یک تصویر مرتبط با آگهی.
/// </summary>
public class AdImage
{
    public int Id { get; set; }
    public string ImagePath { get; set; } = default!;
    public int AdId { get; set; }
    public Ad? Ad { get; set; }
}
