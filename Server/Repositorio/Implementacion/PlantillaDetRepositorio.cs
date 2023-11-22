using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class PlantillaDetRepositorio: IPlantillaDetRepositorio
    {
        private readonly DbQhseContext _dbContext;

        public PlantillaDetRepositorio(DbQhseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<PlantillaDet>> Consultar(Expression<Func<PlantillaDet, bool>> filtro = null)
        {
            IQueryable<PlantillaDet> queryEntidad = filtro == null ? _dbContext.PlantillaDets : _dbContext.PlantillaDets.Where(filtro);
            return queryEntidad;
        }

        public async Task<Creacion> Crear(Creacion entidad)
        {
            try
            {
                _dbContext.Set<Creacion>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(PlantillaDet entidad)
        {
            try
            {
                _dbContext.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<PlantillaDet>> Lista()
        {
            try
            {
                return await _dbContext.PlantillaDets.ToListAsync();
            }
            catch
            {

                throw;
            }
        }

        public async Task<PlantillaDet> Obtener(Expression<Func<PlantillaDet, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.PlantillaDets.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
