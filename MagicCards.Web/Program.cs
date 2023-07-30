using MagicCards.DAL.Models;
using MagicCards.DAL.Respositories;
using MagicCards.MinimalAPI.Models;
using MagicCards.MinimalAPI.Repositories;
using MagicCards.Shared.Mappings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<ICardRepository, SqlCardRepository>();
builder.Services.AddScoped<IDeckRepository, MongoDBDeckRepository>();
builder.Services.AddAutoMapper(new System.Type[]
{
    typeof(CardsProfile),
    typeof(ArtistsProfile),
    typeof(RaritiesProfile),
    typeof(SetsProfile),
    typeof(CardColorsProfile),
    typeof(ColorsProfile),
    typeof(CardTypesProfile),
    typeof(TypesProfile)
});

builder.Services.AddDbContext<mtgContext>(options => options.UseSqlServer(config.GetConnectionString("mtgDb")));
builder.Services.AddHttpClient("CardsAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7103/api/");
});

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddHttpClient("DeckAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7061/api/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

