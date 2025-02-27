﻿using QHSE.Server.Models;
using System.Linq.Expressions;

namespace QHSE.Server.Repositorio.Contrato
{
    public interface ITrabajadorRepositorio
    {
        Task<List<Trabajador>> Lista();
        Task<Creacion> Obtener(Expression<Func<Creacion, bool>> filtro = null);
        Task<Creacion> Crear(Creacion entidad);
        Task<bool> Editar(Creacion entidad);
        Task<IQueryable<Trabajador>> Consultar(Expression<Func<Trabajador, bool>> filtro = null);
    }
}
