namespace QHSE.Client.Servicios.Contrato
{
    public interface IPlantillaService
    {
        Task<ResponseDTO<List<PlantillaDTO>>> Lista();
        Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad);
        Task<bool> Editar(CreacionDTO entidad);
        Task<bool> Eliminar(int id);
    }
}
