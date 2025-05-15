using MudBlazor.Services;
using Refit;
using WebUI.Components;
using WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

var backendUri = new Uri("https://localhost:7160");

builder.Services.AddRefitClient<ICarService>()
    .ConfigureHttpClient(x => x.BaseAddress = backendUri);

builder.Services.AddRefitClient<ITeamService>()
    .ConfigureHttpClient(x => x.BaseAddress = backendUri);

builder.Services.AddRefitClient<ISessionService>()
    .ConfigureHttpClient(x => x.BaseAddress = backendUri);

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