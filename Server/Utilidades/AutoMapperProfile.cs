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
            CreateMap<Creacion, CreacionDTO>();
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
            CreateMap<InspeccionDet, ResumenDTO>()
                .ForMember(destino =>
                   destino.Area,
                   opt => opt.MapFrom(origen => origen.IdInspNavigation.IdAreaNavigation.DescArea))
                 .ForMember(destino =>
                   destino.Empresa,
                   opt => opt.MapFrom(origen => origen.IdInspNavigation.IdEmpNavigation.RazEmp))
                 .ForMember(destino =>
                   destino.Fecha,
                   opt => opt.MapFrom(origen => origen.IdInspNavigation.Fecha))
                 .ForMember(destino =>
                   destino.OpcSelect1,
                   opt => opt.MapFrom(origen => origen.OpcSelect1))
                 .ForMember(destino =>
                   destino.OpcSelect2,
                   opt => opt.MapFrom(origen => origen.OpcSelect2));
            CreateMap<InspeccionDet, InspeccionDetDTO>()
                .ForMember(destino =>
                   destino.IdCtg,
                   opt => opt.MapFrom(origen => origen.IdSubCtgNavigation.IdCtgNavigation.IdCtg))
                .ForMember(destino =>
                   destino.Categoria,
                   opt => opt.MapFrom(origen => origen.IdSubCtgNavigation.IdCtgNavigation.DescCtg))
                 .ForMember(destino =>
                   destino.SubCategoria,
                   opt => opt.MapFrom(origen => origen.IdSubCtgNavigation.DescSubCtg))
                 .ForMember(destino =>
                   destino.NroOrden,
                   opt => opt.MapFrom(origen => origen.NroOrden))
                 .ForMember(destino =>
                   destino.Trabajador,
                   opt => opt.MapFrom(origen => origen.IdInspNavigation.IdSuper1Navigation.NomTraba + " " + origen.IdInspNavigation.IdSuper1Navigation.ApeTraba))
                 .ForMember(destino =>
                   destino.NomSuper2,
                   opt => opt.MapFrom(origen => origen.IdInspNavigation.IdSuper1Navigation.NomTraba + " " + origen.IdInspNavigation.IdSuper1Navigation.ApeTraba))
                 .ForMember(destino =>
                   destino.Area,
                   opt => opt.MapFrom(origen => origen.IdInspNavigation.IdAreaNavigation.DescArea))
                 .ForMember(destino =>
                   destino.Empresa,
                   opt => opt.MapFrom(origen => origen.IdInspNavigation.IdEmpNavigation.RazEmp))
                 .ForMember(destino =>
                   destino.Tipo,
                   opt => opt.MapFrom(origen => origen.IdInspNavigation.IdTpoInspNavigation.Descripcion))
                 .ForMember(destino =>
                   destino.Fecha,
                   opt => opt.MapFrom(origen => origen.IdInspNavigation.Fecha))
                 .ForMember(destino =>
                   destino.NomJefeArea,
                   opt => opt.MapFrom(origen => origen.IdInspNavigation.NomJefeArea))
                 .ForMember(destino =>
                   destino.SI,
                   opt => opt.MapFrom(origen => origen.OpcSelect1=="1"?"X":""))
                 .ForMember(destino =>
                   destino.NO,
                   opt => opt.MapFrom(origen => origen.OpcSelect1 == "0" ? "X" : ""))
                 .ForMember(destino =>
                   destino.NA,
                   opt => opt.MapFrom(origen => origen.OpcSelect1 == "2" ? "X" : ""))
                  .ForMember(destino =>
                   destino.SI2,
                   opt => opt.MapFrom(origen => origen.OpcSelect2 == "1" ? "X" : ""))
                 .ForMember(destino =>
                   destino.NO2,
                   opt => opt.MapFrom(origen => origen.OpcSelect2 == "0" ? "X" : ""))
                 .ForMember(destino =>
                   destino.FirmaJefeArea,
                   opt => opt.MapFrom(origen => origen.IdInspNavigation.FirmaJefeArea))
                 .ForMember(destino =>
                   destino.Firma,
                   opt => opt.MapFrom(origen => origen.IdInspNavigation.IdSuper1Navigation.Firma))

                 ;
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
                    opt => opt.MapFrom(origen => origen.IdSubCtgNavigation.IdCtgNavigation.DescCtg))
                 .ForMember(destino =>
                    destino.IdCtg,
                    opt => opt.MapFrom(origen => origen.IdSubCtgNavigation.IdCtgNavigation.IdCtg))
                 .ForMember(destino =>
                    destino.Area,
                    opt => opt.MapFrom(origen => origen.IdPlantillaNavigation.IdAreaNavigation.DescArea))
                 .ForMember(destino =>
                    destino.Descripcion,
                    opt => opt.MapFrom(origen => origen.IdPlantillaNavigation.Descripcion));
            CreateMap<PlantillaDetDTO, PlantillaDet>();
            #endregion PlantillaDetalle

            #region Area
            CreateMap<Area, AreaDTO>();
            CreateMap<AreaDTO, Area>();
            #endregion Area

            #region Acta
            CreateMap<Actum, ActaDTO>()
                .ForMember(destino =>
                   destino.Activo,
                   opt => opt.MapFrom(origen => origen.Estado==1?"Activo":"Cerrado"));
            CreateMap<ActaDTO, Actum>();
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
