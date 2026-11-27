using DataFilterUI.Components;
using DataFilterUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// HttpClient for DataFilterAPI (base URL set per environment in appsettings*.json)
var apiBaseUrl = builder.Configuration["DataFilterApi:BaseUrl"]
    ?? throw new InvalidOperationException("Missing configuration value 'DataFilterApi:BaseUrl'.");
builder.Services.AddHttpClient<DataFilterApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl.TrimEnd('/') + "/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
