using System.Diagnostics;

namespace WeatherForecastTest.OpenTelemetry
{
    public class TraceResponseMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() => {
                context.Response.Headers.Append("traceresponse", Activity.Current?.Id);
                return Task.FromResult(0);
            });

            await next(context);
        }
    }
}
