namespace DataFilterUI.Services;

/// <summary>
/// Typed HttpClient for DataFilterAPI. The base address comes from "DataFilterApi:BaseUrl" in appsettings.
/// </summary>
public class DataFilterApiClient(HttpClient http)
{
    public Uri? BaseAddress => http.BaseAddress;

    public async Task<WeatherForecast[]> GetWeatherForecastAsync(CancellationToken cancellationToken = default)
        => await http.GetFromJsonAsync<WeatherForecast[]>("WeatherForecast", cancellationToken) ?? [];

    public Task<ApiVersion?> GetVersionAsync(CancellationToken cancellationToken = default)
        => http.GetFromJsonAsync<ApiVersion>("version", cancellationToken);
}

public record WeatherForecast(DateOnly Date, int TemperatureC, int TemperatureF, string? Summary);

public record ApiVersion(string? Version, string? Environment, DateTime StartedUtc);
