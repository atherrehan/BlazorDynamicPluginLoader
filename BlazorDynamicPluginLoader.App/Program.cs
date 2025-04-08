using BlazorDynamicPluginLoader.App.Components;
using BlazorDynamicPluginLoader.App.Models;
using BlazorDynamicPluginLoader.App.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<PluginOptions>(builder.Configuration.GetSection("PluginOptions"));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<IPluginService, PluginService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
