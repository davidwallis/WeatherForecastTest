using WeatherForecastTest.Hosting;
using WeatherForecastTest.OpenTelemetry;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.AddWallis2000OpenTelemetry();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsNotProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddTraceResponseHeader();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
