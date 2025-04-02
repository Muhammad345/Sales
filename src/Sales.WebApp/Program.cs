using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sales.WebApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
var salesApiUrl = builder.Configuration.GetValue<string>("SalesApiUrl");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(salesApiUrl ?? string.Empty) });


await builder.Build().RunAsync();
