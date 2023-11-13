using QHSE.Client.Utilidades;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace QHSE.Client.Servicios.Implementacion
{
    public class CreacionService : ICreacionService
    {
        private readonly HttpClient _http;
        private readonly AppData _appData;

        public CreacionService(HttpClient http, AppData appData)
        {
            _http = http;
            _appData = appData;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appData.usuarioToken);
        }

        public async Task<bool> Anular(CreacionDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/creacion/anular", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<bool>>();

            return response!.status;
        }
    }
}
