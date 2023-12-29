using QHSE.Server.Models;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface IInspeccionRepositorio
    {
        Task<List<Inspeccion>> Lista();
        Task<Creacion> Obtener(Expression<Func<Creacion, bool>> filtro = null);
        Task<Creacion> Crear(Creacion entidad);
        Task<bool> Editar(Creacion entidad);
        Task<IQueryable<Inspeccion>> Consultar(Expression<Func<Inspeccion, bool>> filtro = null);
        Task<IQueryable<InspeccionDet>> ConsultarDetalle(int idInspeccion, int numVerificacion);
    }
}
