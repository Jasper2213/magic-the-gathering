const string commonPrefix = "/api";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBDeckRepository>();

WebApplication app = builder.Build();
ConfigurationManager config = builder.Configuration;
string urlPrefix = config.GetSection("ApiPrefix").Value ?? commonPrefix;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Magic The Gathering").WithTags("API Information");

app.MapGet($"{urlPrefix}/cards", async (MongoDBDeckRepository deckRepo) =>
{
    IEnumerable<DeckItem> cards = await deckRepo.GetAllCards();

    return Results.Ok(cards);
}).WithTags("Get deck");

app.MapPost($"{urlPrefix}/card", async (MongoDBDeckRepository deckRepo, CardReadDTO newCard) =>
{
    await deckRepo.AddCardToDeckAsync(newCard);

    return Results.Ok(newCard);
}).Accepts<Card>("application/json").WithTags("Manage deck");

app.MapDelete($"{urlPrefix}/card", async (MongoDBDeckRepository deckRepo, string id) =>
{
    await deckRepo.DeleteCardAsync(id);

    return Results.Ok();
}).WithTags("Manage deck");

app.MapDelete($"{urlPrefix}/deck", async (MongoDBDeckRepository deckRepo) =>
{
    await deckRepo.ClearDeck();

    return Results.Ok();

}).WithTags("Clear deck");

app.Run();
