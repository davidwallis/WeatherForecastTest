using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WeatherForecastTest.HealthChecks
{
    public static class HealthCheckExtensions
    {
        public static IHealthChecksBuilder AddWallis2000AdvancedHealthCheck(
            this IHealthChecksBuilder builder
        )
        {
            return builder.AddCheck<AdvancedHealthCheck>(nameof(AdvancedHealthCheck));
        }

        public static IEndpointConventionBuilder MapWallis2000HealthChecks(
    this IEndpointRouteBuilder endpoint,
    string endpointUrl = "/healthz"
)
        {
            var endpointConventionBuilder = endpoint.MapHealthChecks(
                endpointUrl,
                new HealthCheckOptions
                {
                    ResponseWriter = WriteResponse
                }
            );
            return endpointConventionBuilder;
        }

        public static Task WriteResponse(
            HttpContext context,
            HealthReport report
        )
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            string json = JsonSerializer.Serialize(
                new
                {
                    Status = report.Status.ToString(),
                    Duration = report.TotalDuration,
                    Info = report.Entries
                        .Select(e =>
                            new
                            {
                                e.Key,
                                e.Value.Description,
                                e.Value.Duration,
                                Status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                                Error = e.Value.Exception?.Message,
                                e.Value.Data,

                                // TODO: Overhead on calling these?
                                ApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0",
                                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name ?? ""
                            })
                        .ToList()
                },
                jsonSerializerOptions);

            context.Response.ContentType = MediaTypeNames.Application.Json;
            return context.Response.WriteAsync(json);
        }
    }
}
