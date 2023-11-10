using QHSE.Shared;
using System.Threading.Tasks;

namespace QHSE.Client.Servicios.Contrato
{
    public interface ITrabajadorService
    {
        Task<ResponseDTO<List<TrabajadorDTO>>> Lista();
    }
}
