using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Serves the OpenAPI document at /openapi/v1.json (all environments, including Azure)
app.MapOpenApi();

// Swagger UI at /swagger, backed by the built-in OpenAPI document
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "DataFilterAPI v1");
    options.DocumentTitle = "DataFilterAPI - Swagger UI";
});

app.UseHttpsRedirection();

// Serves wwwroot/index.html at "/" (test page)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

// Reports which build is running; in CI the version includes the Git commit SHA
var startedUtc = DateTime.UtcNow;
app.MapGet("/version", () => new
{
    version = Assembly.GetExecutingAssembly()
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion,
    environment = app.Environment.EnvironmentName,
    startedUtc
}).ExcludeFromDescription();

app.Run();
