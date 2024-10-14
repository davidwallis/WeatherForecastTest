## Getting Started

### Configuring GitHooks

The following command can be run to configure git to run the appropriate hooks, assuming the working directory is the root of the git repository.

``` git config --local include.path .\.gitconfig ```


## Changes


### Configuration
add configuration file builder


### Opentelemetry
dotnet add package OpenTelemetry # Main library that provides the core OTEL functionality
dotnet add package OpenTelemetry.Extensions.Hosting # Contains extensions to start OpenTelemetry in applications using Microsoft.Extensions.Hosting
dotnet add package OpenTelemetry.Instrumentation.AspNetCore # Instrumentation for ASP.NET Core and Kestrel
dotnet add package OpenTelemetry.Instrumentation.Http # Instrumentation for HttpClient and HttpWebRequest to track outbound HTTP calls
dotnet add package OpenTelemetry.Instrumentation.Runtime # Instrumentation for dotnet runtime
dotnet add package OpenTelemetry.Instrumentation.Process  --prerelease