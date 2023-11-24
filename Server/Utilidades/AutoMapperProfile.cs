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
            CreateMap<Trabajador, TrabajadorDTO>()
                .ForMember(destino =>
                   destino.NombreApellido,
                   opt => opt.MapFrom(origen => origen.NomTraba + ' ' + origen.ApeTraba));
            CreateMap<TrabajadorDTO, Trabajador>();
            #endregion Trabajador

            #region Inspeccion
            CreateMap<Inspeccion, InspeccionDTO>()
                .ForMember(destino =>
                   destino.Area,
                   opt => opt.MapFrom(origen => origen.IdAreaNavigation.DescArea))
                .ForMember(destino =>
                   destino.FechaCrea,
                   opt => opt.MapFrom(origen => origen.IdCreateNavigation.FechaCrea))
                .ForMember(destino =>
                   destino.NomSuper1,
                   opt => opt.MapFrom(origen => origen.IdSuper1Navigation.NomTraba))
                .ForMember(destino =>
                   destino.Inspeccion,
                   opt => opt.MapFrom(origen => origen.IdTpoInspNavigation.Descripcion));
            CreateMap<InspeccionDTO, Inspeccion>();
            #endregion Inspeccion

            #region InspeccionDetalle
            CreateMap<InspeccionDet, InspeccionDetDTO>();
            CreateMap<InspeccionDetDTO, InspeccionDet>();
            #endregion InspeccionDetalle

            #region Plantilla
            CreateMap<Plantilla, PlantillaDTO>()
                 .ForMember(destino =>
                    destino.Area,
                    opt => opt.MapFrom(origen => origen.IdAreaNavigation.DescArea))
                 .ForMember(destino =>
                    destino.FechaCrea,
                    opt => opt.MapFrom(origen => origen.IdCreateNavigation.FechaCrea));
            CreateMap<PlantillaDTO, Plantilla>();
            #endregion Plantilla

            #region PlantillaDetalle
            CreateMap<PlantillaDet, PlantillaDetDTO>()
                 .ForMember(destino =>
                    destino.SubCategoria,
                    opt => opt.MapFrom(origen => origen.IdSubCtgNavigation.DescSubCtg))
                 .ForMember(destino =>
                    destino.Categoria,
                    opt => opt.MapFrom(origen => origen.IdSubCtgNavigation.IdCtgNavigation.DescCtg));
            CreateMap<PlantillaDetDTO, PlantillaDet>();
            #endregion PlantillaDetalle

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

            #region TipoInspeccion
            CreateMap<TpoInspeccion, TipoInspeccionDTO>();
            CreateMap<TipoInspeccionDTO, TpoInspeccion>();
            #endregion TipoInspeccion


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

            #region Empresa
            CreateMap<Empresa, EmpresaDTO>();
            CreateMap<EmpresaDTO, Empresa>();
            #endregion Empresa
        }
    }
}
