using BlazorApp.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // For Blazor Server
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("BackendApi", client =>
{
    var backendUrl = Environment.GetEnvironmentVariable("BACKEND_API_URL");
    client.BaseAddress = new Uri(backendUrl ?? "http://localhost:8080"); // Fallback value
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run(); 