using KasebAPI.Dtos;
using KasebAPI.Models;
using KasebAPI.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Buffers;
using System.Globalization;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace KasebAPI.Services;

/// <summary>
/// پیاده‌سازی کامل سرویس مدیریت آگهی؛ شامل ایجاد، یافتن، جستجو و فیلترینگ.
/// </summary>
public class AdService : IAdService
{
    #region FIELDS & CONSTRUCTOR
    private readonly IAdRepository _repo;
    private readonly IWebHostEnvironment _env;

    public AdService(IAdRepository repo, IWebHostEnvironment env)
    {
        _repo = repo;
        _env = env;
    }
    #endregion

    #region PRIVATE HELPER
    /// <summary>
    /// تبدیل یک `Ad` به `AdDto` (شامل مسیرهای تصاویر).
    /// </summary>
    private string GetImageUrl(string imageFileName) =>
        $"/images/{imageFileName}";

    /// <summary>
    /// پارس string به decimal در زمان فیلترینگ؛ اگر پارس نشد برابر 0 تنظیم می‌شود.
    /// </summary>
    private decimal ToDecimal(string? value) =>
        decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : 0M;

    /// <summary>
    /// مرتب‌سازی برای یک query.
    /// </summary>
    private IQueryable<Ad> ApplySorting(IQueryable<Ad> query, SortAdBy? sort)
    {
        return sort switch
        {
            // Newest => Descending by Id
            SortAdBy.Newest => query.OrderByDescending(a => a.Id),
            SortAdBy.Cheapest => query.OrderBy(a => ToDecimal(a.Price)),
            SortAdBy.MostExpensive => query.OrderByDescending(a => ToDecimal(a.Price)),
            SortAdBy.Heaviest => query.OrderByDescending(a => ToDecimal(a.ValueOfWeighKG)),
            SortAdBy.Lightest => query.OrderBy(a => ToDecimal(a.ValueOfWeighKG)),
            _ => query.OrderByDescending(a => a.Id)
        };
    }
    #endregion

    #region PUBLIC
    /// <summary>
    /// ایجاد یک آگهی و ذخیره تصاویر (در پوشه `/wwwroot/images`).
    /// </summary>
    public async Task<AdDto> CreateAsync(CreateAdDto dto, CancellationToken ct = default)
    {
        /* ---------- ۱) ایجاد آگهی ------------- */
        var ad = new Ad
        {
            Title = dto.Title,
            Content = dto.Content,
            Author = dto.Author,
            City = dto.City,
            Phone = dto.Phone,
            Price = dto.Price,
            Date = dto.Date,
            Category = dto.Category,
            IsUrgent = dto.IsUrgent,
            NonCash = dto.NonCash,
            SomeOfCashMostPayed = dto.SomeOfCashMostPayed,
            MonthForNonCash = dto.MonthForNonCash,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            InventoryGuarantee = dto.InventoryGuarantee,
            ValueOfWeighKG = dto.ValueOfWeighKG,
            ValueOfTag1 = dto.ValueOfTag1,
            ValueOfTag2 = dto.ValueOfTag2
        };

        var createdAd = await _repo.AddAsync(ad, ct);   // id به‌دست می‌آید

        /* ---------- ۲) ذخیره تصاویر ------------- */
        List<string> imageUrls = new();   // این لیست را در خروجی DTO برمی‌گردانیم

        if (dto.Images != null && dto.Images.Any())
        {
            var folder = Path.Combine(_env.WebRootPath, "images");
            Directory.CreateDirectory(folder);

            foreach (var file in dto.Images)
            {
                if (file.Length == 0 || !file.ContentType.StartsWith("image/"))
                    continue;                              // ignore

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(folder, fileName);

                await using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream, ct);

                // ۲a) ذخیره در دیتابیس
                var adImg = new AdImage { AdId = createdAd.Id, ImagePath = fileName };
                await _repo.AddImageAsync(adImg, ct);       // متود جدید

                // ۲b) آماده‌سازی URL خروجی
                imageUrls.Add(GetImageUrl(fileName));
            }
        }

        /* ---------- ۳) تبدیل به DTO ------------- */
        var dtoOut = new AdDto
        {
            Id = createdAd.Id,
            Title = createdAd.Title,
            Content = createdAd.Content,
            Author = createdAd.Author,
            City = createdAd.City,
            Phone = createdAd.Phone,
            Price = createdAd.Price,
            Date = createdAd.Date,
            Category = createdAd.Category,
            IsUrgent = createdAd.IsUrgent,
            NonCash = createdAd.NonCash,
            SomeOfCashMostPayed = createdAd.SomeOfCashMostPayed,
            MonthForNonCash = createdAd.MonthForNonCash,
            Latitude = createdAd.Latitude,
            Longitude = createdAd.Longitude,
            InventoryGuarantee = createdAd.InventoryGuarantee,
            ValueOfWeighKG = createdAd.ValueOfWeighKG,
            ValueOfTag1 = createdAd.ValueOfTag1,
            ValueOfTag2 = createdAd.ValueOfTag2,
            Images = imageUrls
        };

        return dtoOut;
    }

    /// <summary>
    /// جستجوی آگهی بر اساس فیلترها، صفحه‌بندی و مرتب‌سازی.
    /// </summary>
    public async Task<PaginatedResult<AdDto>> SearchAsync(SearchAdsParamsDto filter, CancellationToken ct = default)
    {
        var query = _repo.Query();

        // ۱) فیلتر بر اساس کد مشابه کد قبلی
        if (filter.Category.HasValue && filter.Category.Value != Category.IsNull)
            query = query.Where(a => a.Category == filter.Category.Value);

        if (!string.IsNullOrWhiteSpace(filter.Title))
            query = query.Where(a => a.Title.Contains(filter.Title!, StringComparison.OrdinalIgnoreCase));

        if (filter.City.HasValue && filter.City.Value != Cities.IsNull)
            query = query.Where(a => a.City == filter.City.Value.ToString());

        if (filter.MinPrice.HasValue)
            query = query.Where(a => ToDecimal(a.Price) >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(a => ToDecimal(a.Price) <= filter.MaxPrice.Value);

        if (filter.IsUrgent)
            query = query.Where(a => a.IsUrgent);

        if (filter.IsNonCash)
            query = query.Where(a => a.NonCash);

        if (filter.MinValueOfWeighKG.HasValue)
            query = query.Where(a => ToDecimal(a.ValueOfWeighKG) >= filter.MinValueOfWeighKG.Value);

        if (filter.MaxValueOfWeighKG.HasValue)
            query = query.Where(a => ToDecimal(a.ValueOfWeighKG) <= filter.MaxValueOfWeighKG.Value);

        // ۲) مرتب‌سازی
        query = ApplySorting(query, filter.SortBy);

        // ۳) صفحه بندی
        var totalCount = await query.CountAsync(ct);
        var items = await query.Skip((filter.PageNumber - 1) * filter.PageSize)
                               .Take(filter.PageSize)
                               .ToListAsync(ct);

        var result = items.Select(a => new AdDto
        {
            Id = a.Id,
            Title = a.Title,
            Content = a.Content,
            Author = a.Author,
            City = a.City,
            Phone = a.Phone,
            Price = a.Price,
            Date = a.Date,
            Category = a.Category,
            IsUrgent = a.IsUrgent,
            NonCash = a.NonCash,
            SomeOfCashMostPayed = a.SomeOfCashMostPayed,
            MonthForNonCash = a.MonthForNonCash,
            Latitude = a.Latitude,
            Longitude = a.Longitude,
            InventoryGuarantee = a.InventoryGuarantee,
            ValueOfWeighKG = a.ValueOfWeighKG,
            ValueOfTag1 = a.ValueOfTag1,
            ValueOfTag2 = a.ValueOfTag2,
            Images = a.Images?.Select(img => GetImageUrl(img.ImagePath)).ToList()
          ?? new List<string>()

        }).ToList();

        return new PaginatedResult<AdDto>
        {
            Items = result,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize,
            TotalCount = totalCount
        };
    }

    /// <summary>
    /// دریافت یک آگهی بر اساس شناسه.
    /// </summary>
    public async Task<AdDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var ad = await _repo.GetByIdAsync(id, ct);
        if (ad == null) return null;

        return new AdDto
        {
            Id = ad.Id,
            Title = ad.Title,
            Content = ad.Content,
            Author = ad.Author,
            City = ad.City,
            Phone = ad.Phone,
            Price = ad.Price,
            Date = ad.Date,
            Category = ad.Category,
            IsUrgent = ad.IsUrgent,
            NonCash = ad.NonCash,
            SomeOfCashMostPayed = ad.SomeOfCashMostPayed,
            MonthForNonCash = ad.MonthForNonCash,
            Latitude = ad.Latitude,
            Longitude = ad.Longitude,
            InventoryGuarantee = ad.InventoryGuarantee,
            ValueOfWeighKG = ad.ValueOfWeighKG,
            ValueOfTag1 = ad.ValueOfTag1,
            ValueOfTag2 = ad.ValueOfTag2,
            Images = ad.Images?.Select(img => GetImageUrl(img.ImagePath)).ToList()
          ?? new List<string>()
        };
    }
    #endregion
}
