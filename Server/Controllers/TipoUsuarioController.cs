using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using QHSE.Shared;

namespace QHSE.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITipoUsuarioRepositorio _tipoUsuarioRepositorio;

        public TipoUsuarioController(ITipoUsuarioRepositorio articuloRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _tipoUsuarioRepositorio = articuloRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<TipoUsuarioDTO>> _response = new ResponseDTO<List<TipoUsuarioDTO>>();

            try
            {
                List<TipoUsuarioDTO> _listaTipoUsuarios = new List<TipoUsuarioDTO>();

                IQueryable<TpoUsuario> query = await _tipoUsuarioRepositorio.Consultar();
                query = query.Where(c => c.Activo == 1);

                _listaTipoUsuarios = _mapper.Map<List<TipoUsuarioDTO>>(query.ToList());

                _response = new ResponseDTO<List<TipoUsuarioDTO>>() { status = true, msg = "ok", value = _listaTipoUsuarios };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<TipoUsuarioDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }

           

    }
}
