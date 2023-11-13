using QHSE.Client.Utilidades;
<<<<<<< HEAD
=======
using SistemaPedidos.Client.Utilidades;
using System.Net.Http.Headers;
>>>>>>> 2e36e4e72a9ceccf3d9d1b2d4fac05d43955b108
using System.Net.Http.Json;

namespace QHSE.Client.Servicios.Implementacion
{
<<<<<<< HEAD
    public class TrabajadorService:ITrabajadorService
=======
    public class TrabajadorService : ITrabajadorService
>>>>>>> 2e36e4e72a9ceccf3d9d1b2d4fac05d43955b108
    {
        private readonly HttpClient _http;
        private readonly AppData _appData;

        public TrabajadorService(HttpClient http, AppData appData)
        {
            _http = http;
            _appData = appData;
<<<<<<< HEAD
        }

        public async Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/trabajador/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<CreacionDTO>>();
            return response!;
        }

        public async Task<bool> Editar(CreacionDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/trabajador/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<bool>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/trabajador/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
=======
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appData.usuarioToken);
>>>>>>> 2e36e4e72a9ceccf3d9d1b2d4fac05d43955b108
        }

        public async Task<ResponseDTO<List<TrabajadorDTO>>> Lista()
        {
<<<<<<< HEAD
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<TrabajadorDTO>>>("api/trabajador/Lista");
            return result!;
=======
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<TrabajadorDTO>>>("api/persona/Lista");
            return result;
>>>>>>> 2e36e4e72a9ceccf3d9d1b2d4fac05d43955b108
        }
    }
}
