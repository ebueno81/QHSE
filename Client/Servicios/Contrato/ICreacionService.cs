namespace QHSE.Client.Servicios.Contrato
{
    public interface ICreacionService
    {
        Task<bool> Anular(CreacionDTO entidad);
    }
}
