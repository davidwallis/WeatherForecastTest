using System.Diagnostics;

namespace WeatherForecastTest.OpenTelemetry
{
    public class TraceResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public TraceResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() => {
                context.Response.Headers.Add("traceresponse", Activity.Current?.Id);
                return Task.FromResult(0);
            });

            await _next(context);
        }
    }
}
