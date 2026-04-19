using KasebAPI.Models;
using System.Threading.Tasks;

namespace KasebAPI.Repositories;

/// <summary>
/// انترفیس برای دسترسی به داده‌ی آگهی.
/// </summary>
public interface IAdRepository
{
    Task<Ad> AddAsync(Ad ad, CancellationToken ct = default);
    Task<AdImage> AddImageAsync(AdImage image, CancellationToken ct = default);
    Task<Ad?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
    IQueryable<Ad> Query();                         // برای فیلترینگ در سرویس
}
