using KasebAPI.Dtos;
using System.Threading.Tasks;

namespace KasebAPI.Services;

/// <summary>
/// انترفیس سرویس آگهی؛ مسئول تمام پردازش‌های لایه‌ی business.
/// </summary>
public interface IAdService
{
    Task<AdDto> CreateAsync(CreateAdDto dto, CancellationToken ct = default);
    Task<PaginatedResult<AdDto>> SearchAsync(SearchAdsParamsDto filter, CancellationToken ct = default);
    Task<AdDto?> GetByIdAsync(int id, CancellationToken ct = default);
}
