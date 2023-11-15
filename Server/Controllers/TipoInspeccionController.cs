using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QHSE.Server.Models;
using QHSE.Server.Repositorio.Contrato;
using QHSE.Shared;

namespace QHSE.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TipoInspeccionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITipoInspeccionRepositorio _tipoInspeccionRepositorio;

        public TipoInspeccionController(ITipoInspeccionRepositorio tipoInspeccionRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _tipoInspeccionRepositorio = tipoInspeccionRepositorio;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<TipoInspeccionDTO>> _response = new ResponseDTO<List<TipoInspeccionDTO>>();

            try
            {
                List<TipoInspeccionDTO> _listaTipoUsuarios = new List<TipoInspeccionDTO>();

                IQueryable<TpoInspeccion> query = await _tipoInspeccionRepositorio.Consultar();
                query = query.Where(c => c.Activo == 1);

                _listaTipoUsuarios = _mapper.Map<List<TipoInspeccionDTO>>(query.ToList());

                _response = new ResponseDTO<List<TipoInspeccionDTO>>() { status = true, msg = "ok", value = _listaTipoUsuarios };

                return StatusCode(StatusCodes.Status200OK, _response);
            }
            catch (Exception ex)
            {

                _response = new ResponseDTO<List<TipoInspeccionDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }

        }



    }
}
