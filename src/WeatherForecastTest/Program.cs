using WeatherForecastTest.HealthChecks;
using WeatherForecastTest.Hosting;
using WeatherForecastTest.OpenTelemetry;

//var configuration = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//    .AddJsonFile("secrets/appsettings.secrets.json", optional: true) // https://anthonychu.ca/post/aspnet-core-appsettings-secrets-kubernetes/
//    .AddEnvironmentVariables()
//    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.AddWallis2000OpenTelemetry();



// TODO add three below into single extension method
builder.Services.AddHealthChecks().AddWallis2000AdvancedHealthCheck();

// Create a single instance, so callback runs only once
builder.Services.AddSingleton<AdvancedHealthCheck>();

// Add Health checks
builder.Services.AddHealthChecks().AddWallis2000AdvancedHealthCheck();

// Extend default 5 second shutdown for health checks
builder.Services.Configure<HostOptions>(options =>
{
    options.ShutdownTimeout = System.TimeSpan.FromSeconds(20);
});

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

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
