using KasebAPI.Data;
using KasebAPI.Repositories;
using KasebAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC + JSON (Ignore cycles for navigation)
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// CORS: Allow All (در محیط‌های production باید محدود شود)
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// EF Core – SqlServer
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI – Repository & Service
builder.Services.AddScoped<IAdRepository, AdRepository>();
builder.Services.AddScoped<IAdService, AdService>();

var app = builder.Build();

app.UseStaticFiles();   // wwwroot (images)
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.MapControllers();

app.Run();
