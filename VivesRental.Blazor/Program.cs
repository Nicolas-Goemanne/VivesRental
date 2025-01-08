using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using VivesRental.Sdk;
using VivesRental.Blazor.Settings;
using VivesRental.Authentication;
using VivesRental.Blazor.Stores;
using System.Net.Http.Json;
using System.Text.Json;
using VivesRental.Blazor.Pages;
using VivesRental.Sdk.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using VivesRental.Blazor.Security;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiSettings = new ApiSettings();
builder.Configuration.GetSection(nameof(ApiSettings)).Bind(apiSettings);
builder.Services.AddApi(apiSettings.BaseUrl);

builder.Services.AddScoped<IBearerTokenStore, TokenStore>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();

await builder.Build().RunAsync();


















