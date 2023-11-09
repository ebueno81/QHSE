using QHSE.Client.Utilidades;
using QHSE.Shared;

namespace QHSE.Client.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<ResponseDTO<List<UsuarioDTO>>> Lista();
        Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad);
        Task<bool> Editar(CreacionDTO entidad);
        Task<bool> Eliminar(int id);

        Task<ResponseDTO<List<UsuarioDTO>>> Login(UsuarioDTO entidad);
        Task<ResponseDTO<List<UsuarioDTO>>> Recuperar(UsuarioDTO entidad);
        Task<ResponseDTO<List<UsuarioDTO>>> ValidarDuplicidad(UsuarioDTO entidad);
    }
}
