using ParkingLot.core.Interfaces;
using ParkingLot.Extentions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextAndConfiguration(configuration);
builder.Services.AddServiceLifeTime();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
seedData();

app.UseAuthorization();

app.MapControllers();

app.Run();

void seedData()
{
    using var scope = app.Services.CreateScope();
    var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbIntializer.Initialize().GetAwaiter().GetResult();   
}
