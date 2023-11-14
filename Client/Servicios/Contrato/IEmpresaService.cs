namespace QHSE.Client.Servicios.Contrato
{
    public interface IEmpresaService
    {
        Task<ResponseDTO<List<EmpresaDTO>>> Lista();
        Task<ResponseDTO<EmpresaDTO>> Crear(EmpresaDTO entidad);
        Task<bool> Editar(EmpresaDTO entidad);
        Task<bool> Eliminar(int id);
    }
}
