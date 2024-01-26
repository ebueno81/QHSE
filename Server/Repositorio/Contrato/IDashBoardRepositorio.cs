using QHSE.Server.Models;
using QHSE.Shared;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface IDashBoardRepositorio
    {
        Task<int> TotalInspeccionUltima();
        Task<int> TotalInspeccion();
        Task<int> TotalActas();
        Task<int> TotalPlantillas();
        Task<Dictionary<string, decimal>> InspeccionUltimaSemana();
        
        Task<List<ResumenDTO>> Consolidado(int codigoActa);

    }
}
