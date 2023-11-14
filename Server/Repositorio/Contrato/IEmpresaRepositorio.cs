using QHSE.Server.Models;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface IEmpresaRepositorio
    {
        Task<Empresa> Obtener(Expression<Func<Empresa, bool>> filtro = null);
        Task<Empresa> Crear(Empresa entidad);
        Task<bool> Editar(Empresa entidad);
        Task<IQueryable<Empresa>> Consultar(Expression<Func<Empresa, bool>> filtro = null);
    }
}
