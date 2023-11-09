using QHSE.Server.Models;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface ITrabajadorRepositorio
    {
        Task<List<Trabajador>> Lista();
    }
}
