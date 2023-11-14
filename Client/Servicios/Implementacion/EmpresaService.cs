using QHSE.Client.Utilidades;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace QHSE.Client.Servicios.Implementacion
{
    public class EmpresaService: IEmpresaService
    {
        private readonly HttpClient _http;
        private readonly AppData _appData;

        public EmpresaService(HttpClient http, AppData appData)
        {
            _http = http;
            _appData = appData;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appData.usuarioToken);
        }

        public async Task<ResponseDTO<EmpresaDTO>> Crear(EmpresaDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/Empresa/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<EmpresaDTO>>();
            return response!;
        }

        public async Task<bool> Editar(EmpresaDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/Empresa/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<bool>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/Empresa/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<EmpresaDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<EmpresaDTO>>>("api/Empresa/Lista");
            return result!;
        }
    }
}
