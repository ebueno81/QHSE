using System.Net.Http.Headers;
using System.Net.Http.Json;
using QHSE.Client.Servicios.Contrato;
using QHSE.Client.Utilidades;
using QHSE.Shared;

namespace QHSE.Client.Servicios.Implementacion
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _http;
        private readonly AppData _appData;

        public UsuarioService(HttpClient http, AppData appData)
        {
            _http = http;
            _appData = appData;
        }

        public async Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/usuario/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<CreacionDTO>>();
            return response!;
        }

        public async Task<bool> Editar(CreacionDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/usuario/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<bool>>();

            return response!.status;
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDTO<List<UsuarioDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<UsuarioDTO>>>("api/usuario/Lista");
            return result!;
        }

        public async Task<ResponseDTO<List<UsuarioDTO>>> Login(UsuarioDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/usuario/login", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<List<UsuarioDTO>>>();
            return response;
            
        }

        public async Task<ResponseDTO<List<UsuarioDTO>>> Recuperar(UsuarioDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/usuario/recuperar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<List<UsuarioDTO>>>();
            return response;

        }

        public async Task<ResponseDTO<List<UsuarioDTO>>> ValidarDuplicidad(UsuarioDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/usuario/UsuarioDuplicidad", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<List<UsuarioDTO>>>();
            return response!;
        }
    }
}
