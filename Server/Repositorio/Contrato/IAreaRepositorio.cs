using QHSE.Server.Models;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface IAreaRepositorio
    {
        Task<List<Area>> Lista();
        Task<Creacion> Obtener(Expression<Func<Creacion, bool>> filtro = null);
        Task<Creacion> Crear(Creacion entidad);
        Task<bool> Editar(Creacion entidad);
        Task<IQueryable<Area>> Consultar(Expression<Func<Area, bool>> filtro = null);
    }
}
