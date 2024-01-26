namespace QHSE.Client.Servicios.Contrato
{
    public interface IDashBoardService
    {
        Task<ResponseDTO<DashBoardDTO>> Resumen(int? idActa);
    }
}
