using QHSE.Client.Pages;
using System.Linq.Expressions;

namespace QHSE.Client.Servicios.Contrato
{
    public interface IPlantillaService
    {
        Task<ResponseDTO<List<PlantillaDTO>>> Lista(int? codigoPlantilla, int? tipoBusqueda);
        Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad);
        Task<bool> Editar(CreacionDTO entidad);
        Task<bool> Eliminar(int id);
        Task<ResponseDTO<List<PlantillaDTO>>> Consultar(int codigoPlantilla);
        Task<ResponseDTO<List<PlantillaDetDTO>>> ListaDetalles(int codigoPlantilla);

    }
}
