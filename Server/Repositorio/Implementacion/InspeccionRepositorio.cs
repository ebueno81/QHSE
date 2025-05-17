using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class InspeccionRepositorio:IInspeccionRepositorio
    {
        private readonly DbQhseContext _dbContext;
        private readonly IActaRepositorio _actaRepositorio;

        public InspeccionRepositorio(DbQhseContext dbContext, IActaRepositorio actaRepositorio)
        {
            _dbContext = dbContext;
            _actaRepositorio = actaRepositorio;
            _dbContext.Database.SetCommandTimeout(TimeSpan.FromMinutes(30));
        }

        public async Task<IQueryable<Inspeccion>> Consultar(Expression<Func<Inspeccion, bool>> filtro = null)
        {
            IQueryable<Inspeccion> queryEntidad = filtro == null ? _dbContext.Inspeccions : _dbContext.Inspeccions.Where(filtro);
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

        public async Task<List<Inspeccion>> Lista()
        {
            try
            {
                return await _dbContext.Inspeccions.ToListAsync();
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

        public async Task<IQueryable<InspeccionDet>> ConsultarDetalle(int idInspeccion, int idArea, int numVerificacion)
        {
            // segunda verificacion
            if (numVerificacion== 2)
            {
                IQueryable<InspeccionDet> queryEntidad = _dbContext.InspeccionDets.Where(d => d.IdInsp == idInspeccion && d.OpcSelect1 == "0");
                return queryEntidad;
            }
            else// primera verificacion
            {
                if (numVerificacion == 3)// verificacion anterior 
                {
                    int? penultimoIdActa = await _actaRepositorio.ObtenerPenultimoIdActa();

                    if (penultimoIdActa == null)
                        return Enumerable.Empty<InspeccionDet>().AsQueryable();

                    IQueryable<InspeccionDet> queryEntidad = _dbContext.InspeccionDets.Where(d => d.IdInspNavigation.IdActa == penultimoIdActa && d.IdInspNavigation.IdArea==idArea);
                    return queryEntidad;
                }
                else
                {
                    IQueryable<InspeccionDet> queryEntidad = _dbContext.InspeccionDets.Where(d => d.IdInsp == idInspeccion);
                    return queryEntidad;
                }
                    
            }
            
        }
    }
}
