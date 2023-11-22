using QHSE.Server.Models;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface IPlantillaDetRepositorio
    {
        Task<List<PlantillaDet>> Lista();
        Task<PlantillaDet> Obtener(Expression<Func<PlantillaDet, bool>> filtro = null);
        Task<Creacion> Crear(Creacion entidad);
        Task<bool> Editar(PlantillaDet entidad);
        Task<IQueryable<PlantillaDet>> Consultar(Expression<Func<PlantillaDet, bool>> filtro = null);
    }
}
