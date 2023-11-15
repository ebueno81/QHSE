using QHSE.Server.Models;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface IPlantillaDetRepositorio
    {
        Task<List<PlantillaDet>> Lista();
        Task<Creacion> Obtener(Expression<Func<Creacion, bool>> filtro = null);
        Task<Creacion> Crear(Creacion entidad);
        Task<bool> Editar(Creacion entidad);
        Task<IQueryable<PlantillaDet>> Consultar(Expression<Func<PlantillaDet, bool>> filtro = null);
    }
}
