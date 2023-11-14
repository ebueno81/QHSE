using QHSE.Server.Models;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface ITipoUsuarioRepositorio
    {
        Task<IQueryable<TpoUsuario>> Consultar(Expression<Func<TpoUsuario, bool>> filtro = null);
    }
}
