using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using System.Linq;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class SubCategoriaRepositorio: ISubCategoriaRepositorio
    {
        private readonly DbQhseContext _dbContext;

        public SubCategoriaRepositorio(DbQhseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<SubCategorium>> Consultar(Expression<Func<SubCategorium, bool>> filtro = null)
        {
            IQueryable<SubCategorium> queryEntidad = filtro == null ? _dbContext.SubCategoria : _dbContext.SubCategoria.Where(filtro);
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

        public async Task<List<SubCategorium>> Lista()
        {
            try
            {
                return await _dbContext.SubCategoria.ToListAsync();
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
    }
}
