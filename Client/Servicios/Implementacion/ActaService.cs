using QHSE.Client.Utilidades;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace QHSE.Client.Servicios.Implementacion
{
    public class ActaService : IActaService
    {
        private readonly HttpClient _http;
        private readonly AppData _appData;

        public ActaService(HttpClient http, AppData appData)
        {
            _http = http;
            _appData = appData;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appData.usuarioToken);
        }

        public async Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/acta/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<CreacionDTO>>();
            return response!;
        }

        public async Task<bool> Editar(CreacionDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/acta/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<bool>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/acta/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<ActaDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<ActaDTO>>>("api/acta/Lista");
            return result!;
        }

    }
}
