WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1.1", new OpenApiInfo
    {
        Title = "MTC Howest v1.1",
        Version = "v1.1",
        Description = "Version 1.1 of the MTC Howest API"
    });
    c.SwaggerDoc("v1.5", new OpenApiInfo
    {
        Title = "MTC Howest v1.5",
        Version = "v1.5",
        Description = "Version 1.5 of the MTC Howest API"
    });
});

builder.Services.AddDbContext<mtgContext>
    (options => options.UseSqlServer(config.GetConnectionString("mtgDb")));

builder.Services.AddScoped<ICardRepository, SqlCardRepository>();

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

builder.Services.AddApiVersioning(o =>
{
    o.ReportApiVersions = true;
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 1);
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    }
);

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "MTG Howest v1.1");
        c.SwaggerEndpoint("/swagger/v1.5/swagger.json", "MTG Howest v1.5");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
