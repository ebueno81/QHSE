using QHSE.Client.Utilidades;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace QHSE.Client.Servicios.Implementacion
{
    public class InspeccionService: IInspeccionService
    {
        private readonly HttpClient _http;
        private readonly AppData _appData;

        public InspeccionService(HttpClient http, AppData appData)
        {
            _http = http;
            _appData = appData;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appData.usuarioToken);
        }

        public async Task<ResponseDTO<List<InspeccionDTO>>> Consultar(int codigoInspeccion)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<InspeccionDTO>>>($"api/inspeccion/Obtener?codigoInspeccion={codigoInspeccion}");
            return result!;
        }

        public async Task<ResponseDTO<List<InspeccionDetDTO>>> ListaDetalles(int codigoInspeccion, int codigoArea, int numVerificacion)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<InspeccionDetDTO>>>($"api/inspeccion/ListaDetalles?codigoInspeccion={codigoInspeccion}&codigoArea={codigoArea}&numVerificacion={numVerificacion}");
            return result!;
        }

        public async Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/inspeccion/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<CreacionDTO>>();
            return response!;
        }

        public async Task<bool> Editar(CreacionDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/inspeccion/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<bool>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/inspeccion/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<InspeccionDTO>>> Lista(int? codigoInspeccion)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<InspeccionDTO>>>($"api/inspeccion/Lista?codigoInspeccion={codigoInspeccion}");
            return result!;
        }

        public async Task<ResponseDTO<List<InspeccionDTO>>> ListaFechas(string? fechaInicio, string? fechaFinal)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<InspeccionDTO>>>($"api/inspeccion/ListaFechas?fechaInicio={fechaInicio}&fechaFinal={fechaFinal}");
            return result!;
        }
        
    }
}
