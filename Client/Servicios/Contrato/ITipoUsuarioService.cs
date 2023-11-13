using QHSE.Shared;
using System.Threading.Tasks;

namespace QHSE.Client.Servicios.Contrato
{
    public interface ITipoUsuarioService
    {
        Task<ResponseDTO<List<TpoUsuarioDTO>>> Lista();
    }
}
