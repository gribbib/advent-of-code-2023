var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/days/{day}/parts/{part}", (int day, int part) =>
{
    var assem = typeof(AbstractDays).Assembly;
    var t = assem.GetType($"Day{day}Part{part}");
    if (t == null)
    {
        return Results.NotFound("Type could not be generated from input");
    }

    object abstractDayObject = Activator.CreateInstance(t);
    if (abstractDayObject != null && abstractDayObject is AbstractDays abstractDay)
    {
        abstractDay.FileName = $"../Console/puzzle-inputs/input-puzzle-{day}.txt";
        return Results.Ok(abstractDay.Run());
    }

    return Results.BadRequest("Requested input is not valid");
})
.WithName("GetDaysResult")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
