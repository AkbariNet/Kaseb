using System.Collections.Generic;

/// <summary>
/// نتیجه‌گذاری صفحه‌بندی؛ بر می‌گرداند آیتم‌ها و متادیتا.
/// </summary>
public class PaginatedResult<T>
{
    public IEnumerable<T> Items { get; init; } = Enumerable.Empty<T>();
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
}
