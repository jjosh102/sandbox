using GoogleMaps.Components;
using GoogleMaps.Models;
using GoogleMapsComponents;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<Config>(builder.Configuration.GetSection(nameof(Config)));
var config = builder.Configuration.GetSection(nameof(Config)).Get<Config>();

builder.Services.AddBlazorGoogleMaps(new GoogleMapsComponents.Maps.MapApiLoadOptions(config?.GoogleMapsApiKey ?? throw new InvalidOperationException("Google Maps API Key is not configured."))
    {
        Version = "beta",
        Libraries = "places,visualization,drawing,marker",
    });
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
