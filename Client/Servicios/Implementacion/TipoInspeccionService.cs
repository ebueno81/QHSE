using QHSE.Client.Utilidades;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace QHSE.Client.Servicios.Implementacion
{
    public class TipoInspeccionService: ITipoInspeccionService
    {
        private readonly HttpClient _http;
        private readonly AppData _appData;

        public TipoInspeccionService(HttpClient http, AppData appData)
        {
            _http = http;
            _appData = appData;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appData.usuarioToken);
        }

        public async Task<ResponseDTO<List<TipoInspeccionDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<TipoInspeccionDTO>>>("api/tipoinspeccion/lista");
            return result!;
        }
    }
}
