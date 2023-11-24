using QHSE.Client.Utilidades;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace QHSE.Client.Servicios.Implementacion
{
    public class PlantillaService:IPlantillaService
    {
        private readonly HttpClient _http;
        private readonly AppData _appData;

        public PlantillaService(HttpClient http, AppData appData)
        {
            _http = http;
            _appData = appData;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appData.usuarioToken);
        }

        public async Task<ResponseDTO<List<PlantillaDTO>>> Consultar(int codigoPlantilla)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<PlantillaDTO>>>($"api/plantilla/Obtener?codigoPlantilla={codigoPlantilla}");
            return result!;
        }

        public async Task<ResponseDTO<List<PlantillaDetDTO>>> ListaDetalles(int codigoPlantilla)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<PlantillaDetDTO>>>($"api/plantilla/ListaDetalles?codigoPlantilla={codigoPlantilla}");
            return result!;
        }

        public async Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/plantilla/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<CreacionDTO>>();
            return response!;
        }

        public async Task<bool> Editar(CreacionDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/plantilla/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<bool>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/plantilla/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<PlantillaDTO>>> Lista(int? codigoPlantilla, int? tipoBusqueda)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<PlantillaDTO>>>($"api/plantilla/Lista?codigoPlantilla={codigoPlantilla}&tipoBusqueda={tipoBusqueda}");
            return result!;
        }

   
    }
}
