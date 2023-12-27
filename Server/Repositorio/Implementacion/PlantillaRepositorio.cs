using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class PlantillaRepositorio: IPlantillaRepositorio
    {
        private readonly DbqhseContext _dbContext;

        public PlantillaRepositorio(DbqhseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Plantilla>> Consultar(Expression<Func<Plantilla, bool>> filtro = null)
        {
            IQueryable<Plantilla> queryEntidad = filtro == null ? _dbContext.Plantillas : _dbContext.Plantillas.Where(filtro);
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

        public async Task<bool> Editar(Creacion entidad)
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

        public async Task<List<Plantilla>> Lista()
        {
            try
            {
                return await _dbContext.Plantillas.ToListAsync();
            }
            catch
            {

                throw;
            }
        }

        public async Task<Creacion> Obtener(Expression<Func<Creacion, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.Creacions.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<PlantillaDet>> ConsultarDetalle(int idPlantilla)
        {
            IQueryable<PlantillaDet> queryEntidad = _dbContext.PlantillaDets.Where(d => d.IdPlantilla == idPlantilla);
            return queryEntidad;
        }
    }
}
