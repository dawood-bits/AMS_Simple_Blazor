using AMS.Web.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5173");

// 🔑 Razor + Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();

//sb  
// 🔑 HttpClient with API base URL

// builder.Services.AddHttpClient("api", client =>
// {
//     client.BaseAddress = new Uri("http://localhost:5157/"); // Backend API
// });

builder.Services.AddTransient<AMS.Web.Services.CustomHttpHandler>();

builder.Services.AddHttpClient("AMS.Api", client =>
{
    client.BaseAddress = new Uri("http://localhost:5158/, https://localhost:5157/");
})
.AddHttpMessageHandler<CustomHttpHandler>();

// default HttpClient injection
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("AMS.Api"));


// 🔑 Auth + Custom Provider
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
