using QHSE.Shared;
using System.Threading.Tasks;

namespace QHSE.Client.Servicios.Contrato

{
    public interface ITrabajadorService
    {
        Task<ResponseDTO<List<TrabajadorDTO>>> Lista();


        Task<ResponseDTO<CreacionDTO>> Crear(CreacionDTO entidad);
        Task<bool> Editar(CreacionDTO entidad);
        Task<bool> Eliminar(int id);



    }
}
