using QHSE.Server.Models;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface ISubCategoriaRepositorio
    {
        Task<List<SubCategorium>> Lista();
        Task<Creacion> Obtener(Expression<Func<Creacion, bool>> filtro = null);
        Task<Creacion> Crear(Creacion entidad);
        Task<bool> Editar(Creacion entidad);
        Task<IQueryable<SubCategorium>> Consultar(Expression<Func<SubCategorium, bool>> filtro = null);
    }
}
