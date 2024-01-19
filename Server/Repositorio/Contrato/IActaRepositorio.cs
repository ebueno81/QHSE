using QHSE.Server.Models;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface IActaRepositorio
    {
        Task<List<Actum>> Lista();
        Task<Creacion> Obtener(Expression<Func<Creacion, bool>> filtro = null);
        Task<Creacion> Crear(Creacion entidad);
        Task<bool> Editar(Creacion entidad);
        Task<IQueryable<Actum>> Consultar(Expression<Func<Actum, bool>> filtro = null);
    }
}
