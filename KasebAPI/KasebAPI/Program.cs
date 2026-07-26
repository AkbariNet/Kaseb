using KasebAPI.Data;
using KasebAPI.Models;
using KasebAPI.Models.DTOs;
using KasebAPI.Models.Profile;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
.AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
}); 

builder.Services.AddControllers();/*
builder.Services.AddAutoMapper(assemblies: typeof(MappingProfile),configAction:null);*//*
builder.Services.AddAutoMapper(assemblies: AppDomain.CurrentDomain.GetAssemblies(), configAction: null);*/
builder.Services.AddAutoMapper(
    assemblies: AppDomain.CurrentDomain.GetAssemblies(),
    configAction: (serviceProvider, cfg) =>
    {

        cfg.CreateMap<ProfileModel, ProfileModelDTO.UserProfileDTO>();
        cfg.CreateMap<ProfileModelDTO.UserProfileDTO, ProfileModel>(); // نقشه برعکس

    }
);
builder.Services.AddIdentityCore<ProfileModel>()
.AddEntityFrameworkStores<AppDbContext>()
.AddApiEndpoints();

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentity<ProfileModel, IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

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

/*
app.MapIdentityApi<ProfileModel>();*/

app.MapGet("/test", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}").RequireAuthorization();
app.Run();
