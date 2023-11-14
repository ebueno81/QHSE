using AutoMapper;
using QHSE.Server.Models;
using QHSE.Shared;

namespace QHSE.Server.Utilidades
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() 
        {
            

            
            #region Creacion
            CreateMap<Creacion, CreacionDTO>()
                  .ForMember(destino =>
                destino.Usuarios,
                opt => opt.Ignore());
            CreateMap<CreacionDTO, Creacion>();

            #endregion Creacion

            #region Trabajador
            CreateMap<Trabajador, TrabajadorDTO>();
            CreateMap<TrabajadorDTO, Trabajador>();
            #endregion Trabajador

            #region Area
            CreateMap<Area, AreaDTO>();
            CreateMap<AreaDTO, Area>();
            #endregion Area

            #region Categoria
            CreateMap<Categorium, CategoriaDTO>();
            CreateMap<CategoriaDTO, Categorium>();
            #endregion Categoria

            #region SubCategoria
            CreateMap<SubCategorium, SubCategoriaDTO>()
            .ForMember(destino =>
                    destino.Categoria,
                    opt => opt.MapFrom(origen => origen.IdCtgNavigation.DescCtg));
            CreateMap<SubCategoriaDTO, SubCategorium>();
            #endregion SubCategoria

            #region TipoUsuario
            CreateMap<TpoUsuario, TipoUsuarioDTO>();
            CreateMap<TipoUsuarioDTO, TpoUsuario>();
            #endregion TipoUsuario

            #region Usuario
            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino =>
                    destino.ClaveConfirmacion,
                    opt => opt.MapFrom(origen => origen.ClaveUsua))
                .ForMember(destino =>
                    destino.Correo,
                    opt => opt.MapFrom(origen => origen.IdTrabaNavigation.CorreoTraba))
                .ForMember(destino =>
                    destino.TipoUsuario,
                    opt => opt.MapFrom(origen => origen.IdTpoUsuaNavigation.DescTpo))
                  .ForMember(destino =>
                    destino.NombresApellidos,
                    opt => opt.MapFrom(origen => $"{origen.IdTrabaNavigation.NomTraba} {origen.IdTrabaNavigation.ApeTraba}".Trim())
                );
            #endregion Usuario


        }
    }
}
