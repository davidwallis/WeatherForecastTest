using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WeatherForecastTest.HealthChecks
{
    public class AdvancedHealthCheck : IHealthCheck
    {
        private readonly ILogger<AdvancedHealthCheck>? _logger;
        private HealthCheckResult _result = new(HealthStatus.Healthy);

        public AdvancedHealthCheck(
            IHostApplicationLifetime appLifetime,
            ILogger<AdvancedHealthCheck> logger
        )
        {
            _logger = logger;

            appLifetime.ApplicationStopping.Register(() =>
            {
                _logger?.LogInformation("Application shutting down, Updating health monitor");
                _result = new HealthCheckResult(
                    HealthStatus.Unhealthy,
                    description: "Application shutting down."
                );

                // Delay whilst LB detects box is down
                var shutDownDelay = TimeSpan.FromSeconds(15);
                _logger?.LogInformation(
                    "Delaying application shutdown for {Seconds} seconds",
                    shutDownDelay.TotalSeconds
                );
                Thread.Sleep(shutDownDelay);
            });
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default
        )
        {
            return await Task.FromResult(_result);
        }
    }
}
}
