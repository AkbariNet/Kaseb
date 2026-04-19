using KasebAPI.Data;
using KasebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KasebAPI.Repositories;

public class AdRepository : IAdRepository
{
    private readonly AppDbContext _ctx;
    public AdRepository(AppDbContext ctx) => _ctx = ctx;
    public IQueryable<Ad> Query() => _ctx.Ads.Include(a => a.Images);
    public Task<Ad> AddAsync(Ad ad, CancellationToken ct = default)
    {
        _ctx.Ads.Add(ad);
        return _ctx.SaveChangesAsync(ct).ContinueWith(_ => ad, ct);
    }
    public async Task<AdImage> AddImageAsync(AdImage image, CancellationToken ct = default)
    {
        _ctx.AdImages.Add(image);
        await _ctx.SaveChangesAsync(ct);
        return image;
    }
    public Task<Ad?> GetByIdAsync(int id, CancellationToken ct = default)
        => _ctx.Ads.Include(a => a.Images).FirstOrDefaultAsync(a => a.Id == id, ct);

    public Task<int> CountAsync(CancellationToken ct = default) => _ctx.Ads.CountAsync(ct);
}
