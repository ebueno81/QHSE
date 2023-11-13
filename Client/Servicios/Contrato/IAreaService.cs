namespace QHSE.Client.Servicios.Contrato
{
    public interface IAreaService
    {
        Task<ResponseDTO<List<AreaDTO>>> Lista();
        Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad);
        Task<bool> Editar(CreacionDTO entidad);
        Task<bool> Eliminar(int id);
    }
}
