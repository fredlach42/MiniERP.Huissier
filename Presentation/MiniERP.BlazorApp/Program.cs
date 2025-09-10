using MiniERP.BlazorApp.Components;
using MiniERP.BlazorApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration API pour Clean Architecture
var apiBaseUrl = builder.Configuration["Api:BaseUrl"] ?? "https://localhost:7001/api/";
builder.Services.AddHttpClient<IDossierApiClient, DossierApiClient>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
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

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
