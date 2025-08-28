using Microsoft.EntityFrameworkCore;
using Platform.DataAccess.Postgres;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddOpenApi();
builder.Services.AddDbContext<PlatformDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(PlatformDbContext)));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
