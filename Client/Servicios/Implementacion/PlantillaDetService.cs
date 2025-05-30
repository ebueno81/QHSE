﻿using QHSE.Client.Utilidades;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace QHSE.Client.Servicios.Implementacion
{
    public class PlantillaDetService:IPlantillaDetService
    {
        private readonly HttpClient _http;
        private readonly AppData _appData;

        public PlantillaDetService(HttpClient http, AppData appData)
        {
            _http = http;
            _appData = appData;
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appData.usuarioToken);
        }

        public async Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/plantilladet/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<CreacionDTO>>();
            return response!;
        }

        public async Task<bool> Editar(CreacionDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/plantilladet/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<bool>>();

            return response!.status;
        }

        public async Task<bool> Anular(PlantillaDetDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/plantilladet/Anular",entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<PlantillaDetDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<PlantillaDetDTO>>>("api/plantilladet/Lista");
            return result!;
        }
    }
}
