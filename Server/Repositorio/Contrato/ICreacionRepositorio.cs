using QHSE.Server.Models;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface ICreacionRepositorio
    {
        Task<List<Creacion>> Lista();
        Task<Creacion> Obtener(Expression<Func<Creacion, bool>> filtro = null);
        Task<Creacion> Crear(Creacion entidad);
        Task<bool> Editar(Creacion entidad);
        Task<bool> Eliminar(Creacion entidad);
        Task<IQueryable<Creacion>> Consultar(Expression<Func<Creacion, bool>> filtro = null);
    }
}
