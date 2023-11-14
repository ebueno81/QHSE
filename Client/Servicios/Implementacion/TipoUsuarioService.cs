using QHSE.Client.Utilidades;
using QHSE.Shared;
using SistemaPedidos.Client.Utilidades;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace QHSE.Client.Servicios.Implementacion
{
    public class TipoUsuarioService : ITipoUsuarioService
    {
        private readonly HttpClient _http;
        private readonly AppData _appData;

        public TipoUsuarioService(HttpClient http, AppData appData)
        {
            _http = http;
            _appData = appData;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appData.usuarioToken);
        }

        public async Task<ResponseDTO<List<TipoUsuarioDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<TipoUsuarioDTO>>>("api/tipousuario/lista");
            return result!;
        }
    }
}
