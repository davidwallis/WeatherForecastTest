using global::OpenTelemetry;
using System.Diagnostics;

namespace WeatherForecastTest.OpenTelemetry
{
    internal sealed class HealthMonitorFilteringProcessor : BaseProcessor<Activity>
    {
        public override void OnEnd(Activity activity)
        {
            if (IsHealthEndpoint(activity.DisplayName))
            {
                activity.ActivityTraceFlags &= ~ActivityTraceFlags.Recorded;
            }
        }

        private static bool IsHealthEndpoint(string displayName)
        {
            if (string.IsNullOrEmpty(displayName))
            {
                return false;
            }

            return displayName.StartsWith("/healthz");
        }
    }

}