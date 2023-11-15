namespace QHSE.Client.Servicios.Contrato
{
    public interface ITipoInspeccionService
    {
        Task<ResponseDTO<List<TipoInspeccionDTO>>> Lista();
    }
}
