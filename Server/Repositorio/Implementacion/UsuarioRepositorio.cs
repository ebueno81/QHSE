using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;

namespace QHSE.Server.Repositorio.Implementacion
{
    public class UsuarioRepositorio: IUsuarioRepositorio
    {
        private readonly DbQhseContext _dbContext;

        public UsuarioRepositorio(DbQhseContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IQueryable<Usuario>> Consultar(Expression<Func<Usuario, bool>> filtro = null)
        {
            IQueryable<Usuario> queryEntidad = filtro == null ? _dbContext.Usuarios : _dbContext.Usuarios.Where(filtro);
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



        public async Task<List<Usuario>> Lista()
        {
            try
            {
                return await _dbContext.Usuarios.ToListAsync();
            }
            catch
            {

                throw;
            }
        }

        public async Task<List<Usuario>> Login(string usuario, string clave)
        {


            List<Usuario> listaUsuario = await _dbContext.Usuarios
                .Include(t => t.IdTpoUsuaNavigation)
                .Where(dv => dv.NomUsua == usuario && dv.ClaveUsua == clave)
                .ToListAsync();

            return listaUsuario;
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

        public async  Task<List<Usuario>> ValidarDuplicidad(string usuario)
        {
            List<Usuario> listaUsuario = await _dbContext.Usuarios
               .Include(t => t.IdTpoUsuaNavigation)
               .Where(dv => dv.NomUsua == usuario)
               .ToListAsync();

            return listaUsuario;
        }
    }
}
