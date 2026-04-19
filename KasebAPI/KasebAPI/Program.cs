using KasebAPI.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
.AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddControllers();



builder.Services.AddDbContext<AppDbContext>(options =>

options.UseSqlServer(

builder.Configuration.GetConnectionString("DefaultConnection")

));


builder.Services.AddCors(options =>

{

    options.AddPolicy("AllowAll",

    policy => policy

    .AllowAnyOrigin()

    .AllowAnyHeader()

    .AllowAnyMethod()

    );

});


var app = builder.Build();

app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseDeveloperExceptionPage();
app.MapControllers();


app.Run();