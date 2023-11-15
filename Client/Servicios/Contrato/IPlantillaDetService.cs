namespace QHSE.Client.Servicios.Contrato
{
    public interface IPlantillaDetService
    {
        Task<ResponseDTO<List<PlantillaDetDTO>>> Lista();
        Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad);
        Task<bool> Editar(CreacionDTO entidad);
        Task<bool> Eliminar(int id);
    }
}
