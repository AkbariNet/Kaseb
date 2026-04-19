using KasebAPI.Dtos;
using KasebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KasebAPI.Controllers;

/// <summary>
/// API جهت ایجاد، جستجو و نمایش آگهی.
/// </summary>
[ApiController]
[Route("api/ads")]
public class AdsController : ControllerBase
{
    private readonly IAdService _service;

    public AdsController(IAdService service) => _service = service;

    /// <summary>
    /// ایجاد آگهی (multipart/form‑data).
    /// </summary>
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromForm] CreateAdDto dto, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }


    /// <summary>
    /// دریافت آگهی بر اساس شناسه (GET).
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var ad = await _service.GetByIdAsync(id, ct);
        return ad == null ? NotFound() : Ok(ad);
    }

    /// <summary>
    /// جستجوی آگهی با فیلتر، صفحه بندی، مرتب سازی.
    /// <para>تمام پارامترها از کوئری خوانده می‌شوند.</para>
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync([FromQuery] SearchAdsParamsDto filter,
                                                CancellationToken ct)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _service.SearchAsync(filter, ct);
        return Ok(result);
    }
}
