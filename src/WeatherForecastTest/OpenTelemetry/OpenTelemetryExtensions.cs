using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace WeatherForecastTest.OpenTelemetry
{
    public static class OpenTelemetryExtensions
    {
        public static IApplicationBuilder AddTraceResponseHeader(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TraceResponseMiddleware>();
        }

        public static void AddWallis2000OpenTelemetry(
            this WebApplicationBuilder builder)
        {

            builder.Services.AddOpenTelemetry()
                .ConfigureResource(r => r
                    .AddService(
                        serviceName: builder.Configuration.GetValue("ServiceName", defaultValue: "otel-test")!,
                        serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown",
                        serviceInstanceId: Environment.MachineName))


                .WithTracing(tracing => tracing
                    .AddProcessor<HealthMonitorFilteringProcessor>()
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                )


                .WithMetrics(opts => opts
                    .AddMeter("Polly")
                    .AddRuntimeInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddProcessInstrumentation()
                    .AddPrometheusExporter()
                );


                builder.Services.AddLogging((loggingBuilder => loggingBuilder
                        .ClearProviders()
                        .AddOpenTelemetry(openTelemetry =>
                        {
                            openTelemetry.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName: builder.Configuration.GetValue("ServiceName", defaultValue: "otel-test")!));
                            openTelemetry.IncludeFormattedMessage = true;
                            openTelemetry.IncludeScopes = true;
                            openTelemetry.ParseStateValues = true;
                        })
                    ));

                
        }
    }
}