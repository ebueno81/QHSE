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

builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<ICreacionService, CreacionService>();
builder.Services.AddScoped<ITipoUsuarioService,TipoUsuarioService>();
builder.Services.AddScoped<ITrabajadorService, TrabajadorService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ISubCategoriaService, SubCategoriaService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IPlantillaService, PlantillaService>();
builder.Services.AddScoped<IPlantillaDetService, PlantillaDetService>();


builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AppData>();

builder.Services.AddMudServices();
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
