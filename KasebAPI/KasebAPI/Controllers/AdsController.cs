using KasebAPI.Data;
using KasebAPI.Models;
using KasebAPI.Models.Search;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KasebAPI.Controllers
{
    [ApiController]

    [Route("api/ads")]

    public class AdsController : ControllerBase
    {

        private readonly AppDbContext context;

        private readonly IWebHostEnvironment env;


        public AdsController(AppDbContext context, IWebHostEnvironment env)
        {

            this.context = context;

            this.env = env;

        }






        [HttpPost("create-ad")]

        public async Task<IActionResult> CreateAd([FromForm] CreateAdDto model)

        {

            var ad = new Ad
            {

                Title = model.Title,

                Content = model.Content,

                Author = model.Author,

                City = model.City,

                Phone = model.Phone,

                Price = model.Price,

                Date = model.Date,

                Category = model.Category,

                IsUrgent = model.IsUrgent,

                NonCash = model.NonCash,

                SomeOfCashMostPayed = model.SomeOfCashMostPayed,

                MonthForNonCash = model.MonthForNonCash,

                Latitude = model.Latitude,

                Longitude = model.Longitude,

                InventoryGuarantee = model.InventoryGuarantee,

                ValueOfWeighKG = model.ValueOfWeighKG,

                ValueOfTag1 = model.ValueOfTag1,

                ValueOfTag2 = model.ValueOfTag2

            };


            context.Ads.Add(ad);

            await context.SaveChangesAsync();



            if (model.Images != null && model.Images.Any())

            {

                var folder = Path.Combine(env.WebRootPath, "images");


                if (!Directory.Exists(folder))

                    Directory.CreateDirectory(folder);



                foreach (var file in model.Images)

                {

                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                    var filePath = Path.Combine(folder, fileName);


                    using var stream = new FileStream(filePath, FileMode.Create);

                    await file.CopyToAsync(stream);


                    context.AdImages.Add(new AdImage
                    {

                        AdId = ad.Id,

                        ImagePath = fileName

                    });

                }


                await context.SaveChangesAsync();

            }



            return Ok(ad);

        }





        [HttpGet("MaxAds={MaxValue}")]
        public async Task<IActionResult> GetAds(ushort MaxValue = 1)

        {
            if (MaxValue>10)
            {
                MaxValue = 10;
            }
            if (MaxValue <0)
            {
                MaxValue = 0;

            }

            var ads = await context.Ads

                .Include(x => x.Images)

                .OrderByDescending(x => x.Id)
                .Take(MaxValue)
                .ToListAsync();
            List<Ad> adsForSend = new List<Ad>();


            for (int i = 0; i < ads.Count; i++)
                adsForSend.Add(ads[i]);

            return Ok(adsForSend);

        }

        [HttpGet("MaximumAds={MaxValue}&LastID={LastID}")]

        public async Task<IActionResult> GetAds(ushort MaxValue = 1, uint LastID = 0)

        {
            if (MaxValue > 10)
            {
                MaxValue = 10;
            }
            if (MaxValue < 0)
            {
                MaxValue = 0;

            }
            var ads = await context.Ads
                .Where(x => x.Id < LastID)
                .Include(x => x.Images)

                .OrderByDescending(x => x.Id)
                .Take(MaxValue)
                .ToListAsync();
            List<Ad> adsForSend = new List<Ad>();


            for (int i = 0; i < ads.Count; i++)
                adsForSend.Add(ads[i]);

            return Ok(adsForSend);

        }

        [HttpPost("search/Model={model}")]
        public async Task<IActionResult> SearchAds([FromBody] AgriculturalProductsSearchModel model)
        {
            if (model == null)
            {
                return BadRequest("Search payload is required.");
            }

            // We start with the whole set of ads.
            IQueryable<Ad> query = context.Ads
                                           .Include(a => a.Images); // eager load images

            /* ──────────────────────────────
               1️⃣  Category
            ─────────────────────────────── */
            if (!string.IsNullOrWhiteSpace(model.Category))
            {
                if (Enum.TryParse<Category>(model.Category, ignoreCase: true, out var cat) &&
                    cat != Category.IsNull)               // `IsNull` means “no filter”
                {
                    query = query.Where(a => a.Category == cat);
                }
            }

            /* ──────────────────────────────
               2️⃣  Title (contains, case‑insensitive)
            ─────────────────────────────── */
            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                var title = model.Title.Trim();
                query = query.Where(a => a.Title != null &&
                                         a.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            }

            /* ──────────────────────────────
               3️⃣  City
            ─────────────────────────────── */
            if (!string.IsNullOrWhiteSpace(model.City))
            {
                var city = model.City.Trim();
                query = query.Where(a => a.City != null &&
                                         a.City.Contains(city, StringComparison.OrdinalIgnoreCase));
            }

            /* ──────────────────────────────
               4️⃣  Price (min/max)
            ─────────────────────────────── */
            if (double.TryParse(model.MinPrice, out var minPrice))
            {
                double p ;
                query = query.Where(a => double.TryParse(a.Price, out  p) && p >= minPrice);
            }

            if (double.TryParse(model.MaxPrice, out var maxPrice))
            {
                double p;
                query = query.Where(a => double.TryParse(a.Price, out p) && p <= maxPrice);
            }

            /* ──────────────────────────────
               5️⃣  Urgency & Payment
            ─────────────────────────────── */
            if (model.IsUrgent)
            {
                query = query.Where(a => a.IsUrgent);
            }

            if (model.IsNonCash)
            {
                query = query.Where(a => a.NonCash);
            }

            /* ──────────────────────────────
               6️⃣  Weight (min/max)
            ─────────────────────────────── */
            if (double.TryParse(model.MinValueOfWeighKG, out var minWeight))
            {

                double w;
                query = query.Where(a => double.TryParse(a.ValueOfWeighKG, out w) && w >= minWeight);
            }

            if (double.TryParse(model.MaxValueOfWeighKG, out var maxWeight))
            {
                double w;
                query = query.Where(a => double.TryParse(a.ValueOfWeighKG, out w) && w <= maxWeight);
            }

            /* ──────────────────────────────
               7️⃣  Execute & return
            ─────────────────────────────── */
            var ads = await query.OrderByDescending(a => a.Id).ToListAsync();

            return Ok(ads);    // 200 + JSON payload
        }

    }
}