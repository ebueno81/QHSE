using QHSE.Client.Utilidades;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace QHSE.Client.Servicios.Implementacion
{
    public class DashBoardService : IDashBoardService
    {
        private readonly HttpClient _http;
        private readonly AppData _appData;

        public DashBoardService(HttpClient http, AppData appData)
        {
            _http = http;
            _appData = appData;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appData.usuarioToken);
        }
        public async Task<ResponseDTO<DashBoardDTO>> Resumen(int? idActa)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<DashBoardDTO>>($"api/dashboard/Resumen?idActa={idActa}");
            return result!;
        }
    }
}
