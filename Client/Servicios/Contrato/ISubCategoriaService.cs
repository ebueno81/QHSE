namespace QHSE.Client.Servicios.Contrato
{
    public interface ISubCategoriaService
    {
        Task<ResponseDTO<List<SubCategoriaDTO>>> Lista();
        Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad);
        Task<bool> Editar(CreacionDTO entidad);
        Task<bool> Eliminar(int id);
    }
}
