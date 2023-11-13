global using QHSE.Client.Servicios.Contrato;
global using QHSE.Shared;

using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using QHSE.Client;
using QHSE.Client.Servicios.Implementacion;
using QHSE.Client.Utilidades;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
<<<<<<< HEAD
builder.Services.AddScoped<IAreaService, AreaService>();
=======
builder.Services.AddScoped<ICreacionService, CreacionService>();
builder.Services.AddScoped<ITipoUsuarioService,TipoUsuarioService>();

builder.Services.AddScoped<ITrabajadorService, TrabajadorService>();
>>>>>>> 2e36e4e72a9ceccf3d9d1b2d4fac05d43955b108

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AppData>();

builder.Services.AddMudServices();
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
