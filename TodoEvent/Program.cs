using Microsoft.EntityFrameworkCore;
using TodoEvent.Seeders;
using TodoEvent.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var ConnectionString = builder.Configuration.GetConnectionString("DefaultString");
    options.UseSqlServer(ConnectionString);
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

await EventSeeder.StartSeeder(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
