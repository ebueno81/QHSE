namespace QHSE.Client.Servicios.Contrato
{
    public interface IInspeccionService
    {
        Task<ResponseDTO<List<InspeccionDTO>>> Lista(int? codigoInspeccion);
        Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad);
        Task<bool> Editar(CreacionDTO entidad);
        Task<bool> Eliminar(int id);
        Task<ResponseDTO<List<InspeccionDTO>>> Consultar(int codigoInspeccion);
        Task<ResponseDTO<List<InspeccionDetDTO>>> ListaDetalles(int codigoInspeccion, int numVerifcacion);
    }
}
