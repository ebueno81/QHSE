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
            #endregion Area

            #region Categoria
            CreateMap<Categorium, CategoriaDTO>();
            #endregion Categoria

            //#region Persona
            //CreateMap<Persona, PersonaDTO>();
            //#endregion Persona

            #region TipoUsuario
            CreateMap<TpoUsuario, TpoUsuarioDTO>();
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
