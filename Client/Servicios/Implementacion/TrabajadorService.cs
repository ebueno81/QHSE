using QHSE.Client.Utilidades;
using SistemaPedidos.Client.Utilidades;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace QHSE.Client.Servicios.Implementacion
{
    public class TrabajadorService : ITrabajadorService
    {
        private readonly HttpClient _http;
        private readonly AppData _appData;

        public TrabajadorService(HttpClient http, AppData appData)
        {
            _http = http;
            _appData = appData;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appData.usuarioToken);
        }

        public async Task<ResponseDTO<List<TrabajadorDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<TrabajadorDTO>>>("api/persona/Lista");
            return result;
        }
    }
}
