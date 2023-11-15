using QHSE.Server.Models;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface ITipoInspeccionRepositorio
    {
        Task<IQueryable<TpoInspeccion>> Consultar(Expression<Func<TpoInspeccion, bool>> filtro = null);
    }
}
