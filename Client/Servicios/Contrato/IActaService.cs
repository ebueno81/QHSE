namespace QHSE.Client.Servicios.Contrato
{
    public interface IActaService
    {
        Task<ResponseDTO<List<ActaDTO>>> Lista();
        Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad);
        Task<bool> Editar(CreacionDTO entidad);
        Task<bool> Eliminar(int id);
    }
}
